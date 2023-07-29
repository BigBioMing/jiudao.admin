using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Persistence.Services.Implements.Default;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Service.Sys
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public partial class SysUserService : Service<SysUser>, ISysUserService
    {
        public SysUserService(IRepository<SysUser> currentRepository) : base(currentRepository)
        {
        }
    }
}
