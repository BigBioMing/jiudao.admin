using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Entity.Entities.Sys;
using JDA.Model.Sys.SysLogs;
using JDA.Model.Sys.SysUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.IService.Sys
{
    /// <summary>
    /// HTTP请求响应日志
    /// </summary>
    public partial interface ISysLogService : IService<SysLog>
    {
        /// <summary>
        /// 保存日志信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<OperationResult<SysLog>> SaveAsync(SysLog model);
    }
}
