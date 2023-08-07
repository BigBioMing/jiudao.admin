using AutoMapper;
using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Persistence.Services.Implements.Default;
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
        protected readonly IMapper _mapper;
        protected readonly IRepository<SysRoleRouteResource> _sysRoleRouteResourceRepository;
        public SysRoleService(
            IRepository<SysRole> currentRepository
            , IMapper mapper
            , IRepository<SysRoleRouteResource> sysRoleRouteResourceRepository
            ) : base(currentRepository)
        {
            this._mapper = mapper;
            this._sysRoleRouteResourceRepository = sysRoleRouteResourceRepository;
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
                var middles = await this._sysRoleRouteResourceRepository.GetEntities(n => n.RoleId == role.Id).ToListAsync();
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
    }
}
