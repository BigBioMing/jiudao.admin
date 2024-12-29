using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.DTO.SysUsers;
using JDA.Entity.Entities.Sys;
using JDA.Model.Sys.SysUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.IService.Sys
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public partial interface ISysUserService : IService<SysUser>
    {
        /// <summary>
        /// 查询用户角色
        /// </summary>
        /// <param name="user">要查询的用户</param>
        /// <returns></returns>
        Task<OperationResult<List<SysRole>>> GetRolesAsync(SysUser user);

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<OperationResult<SysUser>> SaveAsync(SysUserSaveVO model);

        /// <summary>
        /// 获取用户拥有的按钮和菜单权限
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<UserMenuAndActionDto> GetMenuAndActions(long userId);
    }
}
