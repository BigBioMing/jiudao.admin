using AutoMapper;
using JDA.Core.Formats.WebApi;
using JDA.Core.Mappers.Abstractions;
using JDA.Core.Models.ApiModelErrors;
using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Persistence.Services.Implements.Default;
using JDA.DTO.SysActionResources;
using JDA.DTO.SysUsers;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using JDA.Model.Sys.SysDictionaryDatas;
using JDA.Model.Sys.SysUsers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Service.Sys
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public partial class SysUserService : Service<SysUser>, ISysUserService
    {
        protected readonly IRepository<SysUserRole> _sysUserRoleRepository;
        protected readonly IRepository<SysUserOrganization> _sysUserOrganizationRepository;
        protected readonly IRepository<SysRoleActionResource> _sysRoleActionResourceRepository;
        protected readonly IRepository<SysRoleRouteResource> _sysRoleRouteResourceRepository;
        protected readonly IRepository<SysActionResource> _sysActionResourceRepository;
        protected readonly IRepository<SysRouteResource> _sysRouteResourceRepository;
        protected readonly ISysRoleService _sysRoleService;

        public SysUserService(
            IShapeMapper mapper
            , IRepository<SysUser> currentRepository
            , IRepository<SysUserRole> sysUserRoleRepository
            , IRepository<SysUserOrganization> sysUserOrganizationRepository
            , IRepository<SysRoleActionResource> sysRoleActionResourceRepository
            , IRepository<SysRoleRouteResource> sysRoleRouteResourceRepository
            , IRepository<SysActionResource> sysActionResourceRepository
            , IRepository<SysRouteResource> sysRouteResourceRepository
            , ISysRoleService sysRoleService
            ) : base(mapper, currentRepository)
        {
            _sysUserRoleRepository = sysUserRoleRepository;
            _sysUserOrganizationRepository = sysUserOrganizationRepository;
            _sysRoleActionResourceRepository = sysRoleActionResourceRepository;
            _sysRoleRouteResourceRepository = sysRoleRouteResourceRepository;
            _sysActionResourceRepository = sysActionResourceRepository;
            _sysRouteResourceRepository = sysRouteResourceRepository;
            _sysRoleService = sysRoleService;
        }

        /// <summary>
        /// 查询用户角色
        /// </summary>
        /// <param name="user">要查询的用户</param>
        /// <returns></returns>
        public virtual async Task<OperationResult<List<SysRole>>> GetRolesAsync(SysUser user)
        {
            //获取用户关联的角色Id
            var roleIds = (await this._sysUserRoleRepository.QueryableNoTracking.Where(n => n.UserId == user.Id).Select(n => n.RoleId).ToArrayAsync()).Distinct().ToArray();
            if (roleIds.Length == 0) return OperationResult<List<SysRole>>.Success();

            //根据角色Id查询出关联的角色
            var roles = await this._sysRoleService.GetEntitiesAsync(n => roleIds.Contains(n.Id));
            return OperationResult<List<SysRole>>.Success(roles);
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<OperationResult<SysUser>> SaveAsync(SysUserSaveVO model)
        {
            SysUser user = _mapper.Map<SysUser>(model);
            //var a = this._currentRepository.DbContext.Entry<SysUser>(user).Property(n => n.Name);
            //var b = this._currentRepository.DbContext.Entry<SysUser>(user).Property(n => new { n.Password, n.Name });
            //this._currentRepository.DbContext.Entry<SysUser>(user).Property(n => n.Name).IsModified = true;
            //this._currentRepository.DbContext.Entry<SysUser>(user).Property(n => new { n.Password, n.Name }).IsModified = true;

            OperationResult<SysUser> operationResult;
            //Id>0即为修改，Id<0即为添加
            if (model.Id > 0)
            {
                operationResult = await base.UpdateAsync(user, n => new { n.Name, n.Password });
                //operationResult = await base.UpdateAsync(user);
            }
            else
            {
                user.PasswordSalt = Guid.NewGuid().ToString("N");
                operationResult = await base.InsertAsync(user);
            }

            if (operationResult.Status != JDA.Core.Models.Operations.OperationResultStatus.Success)
                return operationResult;

            #region 保存用户与角色的中间表
            {
                //保存用户的角色
                var middles = await this._sysUserRoleRepository.GetEntitiesAsync(n => n.UserId == user.Id);
                var middleIds = middles.Select(n => n.RoleId).ToList();
                //需要删除的中间表
                var delMiddles = middles.Where(n => model.RoleIds?.Contains(n.RoleId) != true).ToList();
                if (delMiddles?.Count > 0)
                {
                    var delOperationResult = await this._sysUserRoleRepository.DeleteAsync(delMiddles);
                    if (delOperationResult.Status != OperationResultStatus.Success)
                        return OperationResult<SysUser>.Error("保存用户角色失败[0]");
                }
                //需要添加的中间表
                var addMiddles = model.RoleIds?.Where(roleId => middleIds?.Contains(roleId) != true).Select(roleId => new SysUserRole()
                {
                    UserId = user.Id,
                    RoleId = roleId,
                }).ToList();
                if (addMiddles?.Count > 0)
                {
                    var addMidOperationResult = await this._sysUserRoleRepository.InsertAsync(addMiddles);
                    if (addMidOperationResult.Status != OperationResultStatus.Success)
                        return OperationResult<SysUser>.Error("保存用户角色失败[1]");
                }
            }
            #endregion

            #region 保存用户与所属机构的中间表
            {
                //保存用户的所属机构
                var middles = await this._sysUserOrganizationRepository.GetEntitiesAsync(n => n.UserId == user.Id);
                var middleIds = middles.Select(n => n.OrgId).ToList();
                //需要删除的中间表
                var delMiddles = middles.Where(n => model.OrgIds?.Contains(n.OrgId) != true).ToList();
                if (delMiddles?.Count > 0)
                {
                    var delOperationResult = await this._sysUserOrganizationRepository.DeleteAsync(delMiddles);
                    if (delOperationResult.Status != OperationResultStatus.Success)
                        return OperationResult<SysUser>.Error("保存用户所属机构失败[0]");
                }
                //需要添加的中间表
                var addMiddles = model.OrgIds?.Where(orgId => middleIds?.Contains(orgId) != true).Select(orgId => new SysUserOrganization()
                {
                    UserId = user.Id,
                    OrgId = orgId,
                }).ToList();
                if (addMiddles?.Count > 0)
                {
                    var addMidOperationResult = await this._sysUserOrganizationRepository.InsertAsync(addMiddles);
                    if (addMidOperationResult.Status != OperationResultStatus.Success)
                        return OperationResult<SysUser>.Error("保存用户所属机构失败[1]");
                }
            }
            #endregion

            return OperationResult<SysUser>.Success();
        }


        /// <summary>
        /// 获取用户拥有的按钮和菜单权限
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public virtual async Task<UserMenuAndActionDto> GetMenuAndActions(long userId)
        {
            //获取该用户的角色sql
            var roleQuery = from a in _sysUserRoleRepository.QueryableNoTracking.Where(n => n.UserId == userId)
                            join b in _sysRoleService.QueryableNoTracking on a.RoleId equals b.Id
                            select b;

            //获取该用户的菜单sql
            var menuQuery = from a in roleQuery
                            join b in _sysRoleRouteResourceRepository.QueryableNoTracking on a.Id equals b.RoleId
                            join c in _sysRouteResourceRepository.QueryableNoTracking on b.RouteResourceId equals c.Id
                            select c;

            //获取该用户的按钮sql
            var actionQuery = from a in roleQuery
                              join b in _sysRoleActionResourceRepository.QueryableNoTracking on a.Id equals b.RoleId
                              join c in _sysActionResourceRepository.QueryableNoTracking on b.ActionResourceId equals c.Id
                              select c;

            var menus = await menuQuery.OrderBy(n => n.Sort).ToListAsync();
            var actions = await actionQuery.ToListAsync();
            var actions2 = _mapper.Map<List<SysActionResourceDto>>(actions);

            //获取菜单树
            var topMenuTreeNodes = LoopMenus(menus, null);

            return new UserMenuAndActionDto() { MenuTreeNodes = topMenuTreeNodes, Actions = actions2 };
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="allMenus">所有菜单</param>
        /// <param name="parentMenu">父级菜单（为null时表示从1级菜单开始获取</param>
        /// <returns></returns>
        private List<MenuTreeDto> LoopMenus(List<SysRouteResource> allMenus, SysRouteResource? parentMenu)
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
                //获取下下级菜单
                var grandsonMenuTreeNodes = LoopMenus(allMenus, currentMenu);
                childMenuTreeNode.Childrens = grandsonMenuTreeNodes;
            }

            return childMenuTreeNodes;
        }
    }
}
