using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Services.Abstractions.Default;
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
        /// <param name="model"></param>
        /// <returns></returns>
        Task<OperationResult> Empower(SysRoleEmpowerVO model);
    }
}
