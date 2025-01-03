using AutoMapper;
using JDA.Core.Mappers.Abstractions;
using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Persistence.Services.Implements.Default;
using JDA.DTO.SysActionResources;
using JDA.DTO.SysRoles;
using JDA.DTO.SysUsers;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using JDA.Model.Sys.SysRoles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Service.Sys
{
    /// <summary>
    /// 角色
    /// </summary>
    public class SysRoleService : Service<SysRole>, ISysRoleService
    {
        protected readonly IRepository<SysUserRole> _sysUserRoleRepository;
        protected readonly IRepository<SysUserOrganization> _sysUserOrganizationRepository;
        protected readonly IRepository<SysRoleActionResource> _sysRoleActionResourceRepository;
        protected readonly IRepository<SysRoleRouteResource> _sysRoleRouteResourceRepository;
        protected readonly IRepository<SysActionResource> _sysActionResourceRepository;
        protected readonly IRepository<SysRouteResource> _sysRouteResourceRepository;
        public SysRoleService(
            IShapeMapper mapper
            , IRepository<SysRole> currentRepository
            , IRepository<SysUserRole> sysUserRoleRepository
            , IRepository<SysUserOrganization> sysUserOrganizationRepository
            , IRepository<SysRoleActionResource> sysRoleActionResourceRepository
            , IRepository<SysRoleRouteResource> sysRoleRouteResourceRepository
            , IRepository<SysActionResource> sysActionResourceRepository
            , IRepository<SysRouteResource> sysRouteResourceRepository
            ) : base(mapper, currentRepository)
        {
            this._sysUserRoleRepository = sysUserRoleRepository;
            this._sysUserOrganizationRepository = sysUserOrganizationRepository;
            this._sysRoleActionResourceRepository = sysRoleActionResourceRepository;
            this._sysRoleRouteResourceRepository = sysRoleRouteResourceRepository;
            this._sysActionResourceRepository = sysActionResourceRepository;
            this._sysRouteResourceRepository = sysRouteResourceRepository;
        }

        /// <summary>
        /// 给角色授权
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="routeResourceIds">路由资源Id集合（菜单Id）</param>
        /// <param name="actionResourceIds">操作资源Id集合（按钮Id）</param>
        /// <returns></returns>
        public async Task<OperationResult> Empower(long roleId, List<long> routeResourceIds, List<long> actionResourceIds)
        {
            var role = await this.FirstOrDefaultAsync(n => n.Id == roleId);
            if (role is null) return OperationResult.Error("角色不存在");

            #region 保存角色与路由资源的中间表
            {
                //保存角色的路由资源
                var middles = await this._sysRoleRouteResourceRepository.GetEntitiesAsync(n => n.RoleId == role.Id);
                var middleRouteIds = middles.Select(n => n.RouteResourceId).ToList();
                //需要删除的中间表
                var delMiddles = middles.Where(n => routeResourceIds?.Contains(n.RouteResourceId) != true).ToList();
                if (delMiddles?.Count > 0)
                {
                    var delOperationResult = await this._sysRoleRouteResourceRepository.DeleteAsync(delMiddles);
                    if (delOperationResult.Status != OperationResultStatus.Success)
                        return OperationResult<SysUser>.Error("给角色授权失败[0]");
                }
                //需要添加的中间表
                var addMiddles = routeResourceIds?.Where(routeResourceId => middleRouteIds?.Contains(routeResourceId) != true).Select(routeResourceId => new SysRoleRouteResource()
                {
                    RoleId = role.Id,
                    RouteResourceId = routeResourceId
                }).ToList();
                if (addMiddles?.Count > 0)
                {
                    var addMidOperationResult = await this._sysRoleRouteResourceRepository.InsertAsync(addMiddles);
                    if (addMidOperationResult.Status != OperationResultStatus.Success)
                        return OperationResult<SysUser>.Error("给角色授权失败[1]");
                }
            }
            #endregion

            #region 保存角色与操作资源的中间表
            {
                //保存角色的操作资源
                var middles = await this._sysRoleActionResourceRepository.GetEntitiesAsync(n => n.RoleId == role.Id);
                var middleActionIds = middles.Select(n => n.ActionResourceId).ToList();
                //需要删除的中间表
                var delMiddles = middles.Where(n => actionResourceIds?.Contains(n.ActionResourceId) != true).ToList();
                if (delMiddles?.Count > 0)
                {
                    var delOperationResult = await this._sysRoleActionResourceRepository.DeleteAsync(delMiddles);
                    if (delOperationResult.Status != OperationResultStatus.Success)
                        return OperationResult<SysUser>.Error("给角色授权失败[2]");
                }
                //需要添加的中间表
                var addMiddles = actionResourceIds?.Where(actionResourceId => middleActionIds?.Contains(actionResourceId) != true).Select(actionResourceId => new SysRoleActionResource()
                {
                    RoleId = role.Id,
                    ActionResourceId = actionResourceId
                }).ToList();
                if (addMiddles?.Count > 0)
                {
                    var addMidOperationResult = await this._sysRoleActionResourceRepository.InsertAsync(addMiddles);
                    if (addMidOperationResult.Status != OperationResultStatus.Success)
                        return OperationResult<SysUser>.Error("给角色授权失败[3]");
                }
            }
            #endregion

            return OperationResult.Success();
        }


        #region 获取菜单和按钮
        /// <summary>
        /// 获取菜单和按钮
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public virtual async Task<MenuAndActionDto> GetMenuAndActions(long roleId)
        {
            var allMenus = await _sysRouteResourceRepository.QueryableNoTracking.OrderBy(n => n.Sort).ToListAsync();
            var allActions = await _sysActionResourceRepository.QueryableNoTracking.OrderBy(n => n.Sort).ToListAsync();


            //该角色拥有的菜单权限
            var roleRouteIds = await _sysRoleRouteResourceRepository.QueryableNoTracking.Where(n => n.RoleId == roleId).Select(n => n.RouteResourceId).ToListAsync();
            //该角色拥有的按钮权限
            var roleActionIds = await _sysRoleActionResourceRepository.QueryableNoTracking.Where(n => n.RoleId == roleId).Select(n => n.ActionResourceId).ToListAsync();

            //获取菜单树
            var topMenuTreeNodes = LoopMenus(allMenus, allActions, roleRouteIds, roleActionIds, null);

            return new MenuAndActionDto() { MenuTreeNodes = topMenuTreeNodes, SelectMenuIds = roleRouteIds, SelectActionIds = roleActionIds };
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="allMenus">所有菜单</param>
        /// <param name="allActions">所有按钮</param>
        /// <param name="roleRouteIds">该角色拥有的菜单权限</param>
        /// <param name="roleActionIds">该角色拥有的按钮权限</param>
        /// <param name="parentMenu">父级菜单（为null时表示从1级菜单开始获取</param>
        /// <returns></returns>
        private List<MenuTreeDto> LoopMenus(List<SysRouteResource> allMenus, List<SysActionResource> allActions, List<long> roleRouteIds, List<long> roleActionIds, SysRouteResource? parentMenu)
        {
            List<MenuTreeDto> childMenuTreeNodes = new List<MenuTreeDto>();
            //根据ParentId获取下级菜单，然后组装数据
            long parentId = parentMenu?.Id ?? 0;
            var currentMenus = allMenus.Where(n => n.ParentId == parentId).OrderBy(n => n.Sort).ToList();
            foreach (var currentMenu in currentMenus)
            {
                //组装子级节点数据
                MenuTreeDto childMenuTreeNode = this._mapper.Map<MenuTreeDto>(currentMenu);
                childMenuTreeNodes.Add(childMenuTreeNode);
                //获取该菜单下的按钮
                var currrentMenusActions = allActions.Where(n => n.RouteResourceId == currentMenu.Id).ToList();
                childMenuTreeNode.Actions = new List<ActionTreeDto>();

                childMenuTreeNode.IsCheck = roleRouteIds.Any(n => n == currentMenu.Id);

                foreach (var currrentMenusAction in currrentMenusActions)
                {
                    var currrentMenusActionTreeNode = _mapper.Map<ActionTreeDto>(currrentMenusAction);
                    childMenuTreeNode.Actions.Add(currrentMenusActionTreeNode);
                    currrentMenusActionTreeNode.IsCheck = roleActionIds.Any(n => n == currrentMenusAction.Id);
                }

                //获取下下级菜单
                var grandsonMenuTreeNodes = LoopMenus(allMenus, allActions, roleRouteIds, roleActionIds, currentMenu);
                childMenuTreeNode.Childrens = grandsonMenuTreeNodes;
                if (childMenuTreeNode.Childrens?.Count == 0) childMenuTreeNode.Childrens = null;
            }

            return childMenuTreeNodes;
        }
        #endregion
    }
}
