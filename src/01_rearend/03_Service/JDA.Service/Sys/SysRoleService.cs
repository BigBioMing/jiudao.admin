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
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<OperationResult> Empower(SysRoleEmpowerVO model)
        {
            var role = await this.FirstOrDefaultAsync(n => n.Id == model.RoleId);
            if (role is null) return OperationResult.Error("角色不存在");

            #region 保存角色与路由资源的中间表
            {
                //保存角色的路由资源
                var middles = await this._sysRoleRouteResourceRepository.GetEntitiesAsync(n => n.RoleId == role.Id);
                var middleIds = middles.Select(n => n.RoleId).ToList();
                //需要删除的中间表
                var delMiddles = middles.Where(n => model.RoleRouteResourceIds?.Contains(n.RoleId) != true).ToList();
                if (delMiddles?.Count > 0)
                {
                    var delOperationResult = await this._sysRoleRouteResourceRepository.DeleteAsync(delMiddles);
                    if (delOperationResult.Status != OperationResultStatus.Success)
                        return OperationResult<SysUser>.Error("给角色授权失败[0]");
                }
                //需要添加的中间表
                var addMiddles = model.RoleRouteResourceIds?.Where(routeResourceId => middleIds?.Contains(routeResourceId) != true).Select(routeResourceId => new SysRoleRouteResource()
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

            return OperationResult.Success();
        }


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

            //选中的菜单Id集合
            List<long> selectMenuIds = new List<long>();
            //获取菜单树
            var topMenuTreeNodes = LoopMenus(allMenus, allActions, roleRouteIds, roleActionIds, null, selectMenuIds);

            return new MenuAndActionDto() { MenuTreeNodes = topMenuTreeNodes, SelectMenuIds = selectMenuIds };
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
        private List<MenuTreeDto> LoopMenus(List<SysRouteResource> allMenus, List<SysActionResource> allActions, List<long> roleRouteIds, List<long> roleActionIds, SysRouteResource? parentMenu, List<long> selectMenuIds)
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
                if (childMenuTreeNode.IsCheck) selectMenuIds.Add(currentMenu.Id);

                foreach (var currrentMenusAction in currrentMenusActions)
                {
                    var currrentMenusActionTreeNode = _mapper.Map<ActionTreeDto>(currrentMenusAction);
                    childMenuTreeNode.Actions.Add(currrentMenusActionTreeNode);
                    currrentMenusActionTreeNode.IsCheck = roleActionIds.Any(n => n == currrentMenusAction.Id);
                }

                //获取下下级菜单
                var grandsonMenuTreeNodes = LoopMenus(allMenus, allActions, roleRouteIds, roleActionIds, currentMenu, selectMenuIds);
                childMenuTreeNode.Childrens = grandsonMenuTreeNodes;
                if (childMenuTreeNode.Childrens?.Count == 0) childMenuTreeNode.Childrens = null;
            }

            return childMenuTreeNodes;
        }
    }
}
