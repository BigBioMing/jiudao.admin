using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.DTO.SysRoles;
using JDA.Entity.Entities.Sys;
using JDA.Model.Sys.SysRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.IService.Sys
{
    /// <summary>
    /// 角色
    /// </summary>
    public interface ISysRoleService : IService<SysRole>
    {
        /// <summary>
        /// 给角色授权
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="routeResourceIds">路由资源Id集合（菜单Id）</param>
        /// <param name="actionResourceIds">操作资源Id集合（按钮Id）</param>
        /// <returns></returns>
        Task<OperationResult> Empower(long roleId, List<long> routeResourceIds, List<long> actionResourceIds);
        /// <summary>
        /// 获取菜单和按钮
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        Task<MenuAndActionDto> GetMenuAndActions(long roleId);
    }
}
