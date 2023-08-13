using AutoMapper;
using JDA.Core.Formats.WebApi;
using JDA.Core.Mappers.Abstractions;
using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Persistence.Services.Implements.Default;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using JDA.Model.Sys.SysUsers;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public SysUserService(
            IShapeMapper mapper
            , IRepository<SysUser> currentRepository
            , IRepository<SysUserRole> sysUserRoleRepository
            , IRepository<SysUserOrganization> sysUserOrganizationRepository
            ) : base(mapper, currentRepository)
        {
            this._sysUserRoleRepository = sysUserRoleRepository;
            this._sysUserOrganizationRepository = sysUserOrganizationRepository;
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<OperationResult<SysUser>> SaveAsync(SysUserSaveVO model)
        {
            SysUser user = _mapper.Map<SysUser>(model);
            OperationResult<SysUser> operationResult;
            //Id>0即为修改，Id<0即为添加
            if (model.Id > 0)
            {
                operationResult = await base.UpdateAsync(user);
            }
            else
            {
                operationResult = await base.InsertAsync(user);
            }

            if (operationResult.Status != JDA.Core.Models.Operations.OperationResultStatus.Success)
                return operationResult;

            #region 保存用户与角色的中间表
            {
                //保存用户的角色
                var middles = await this._sysUserRoleRepository.GetEntities(n => n.UserId == user.Id).ToListAsync();
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
                var middles = await this._sysUserOrganizationRepository.GetEntities(n => n.UserId == user.Id).ToListAsync();
                var middleIds = middles.Select(n => n.OrgId).ToList();
                //需要删除的中间表
                var delMiddles = middles.Where(n => model.RoleIds?.Contains(n.OrgId) != true).ToList();
                if (delMiddles?.Count > 0)
                {
                    var delOperationResult = await this._sysUserOrganizationRepository.DeleteAsync(delMiddles);
                    if (delOperationResult.Status != OperationResultStatus.Success)
                        return OperationResult<SysUser>.Error("保存用户所属机构失败[0]");
                }
                //需要添加的中间表
                var addMiddles = model.RoleIds?.Where(orgId => middleIds?.Contains(orgId) != true).Select(orgId => new SysUserOrganization()
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
    }
}
