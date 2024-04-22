using JDA.Core.Models.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.HttpLoggings
{
    /// <summary>
    /// HTTP请求响应日志存储接口
    /// </summary>
    public partial interface IHttpLoggingStorage
    {
        /// <summary>
        /// 保存日志信息
        /// </summary>
        /// <param name="logInfo">日志信息</param>
        /// <returns></returns>
        Task<OperationResult> SaveAsync(HttpLoggingInformation logInfo);
        /// <summary>
        /// 保存日志信息
        /// </summary>
        /// <param name="list">日志信息集合</param>
        /// <returns></returns>
        Task<OperationResult> SaveAsync(List<HttpLoggingInformation> list);
    }
}
