using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Users.Distinguish
{
    /// <summary>
    /// 应用程序类型
    /// </summary>
    public enum ApplicationType
    {
        /// <summary>
        /// 后台服务
        /// </summary>
        [Description("后台服务")]
        HostedService = 0,
        /// <summary>
        /// WebApi
        /// </summary>
        [Description("WebApi")]
        WebApi = 1,
        /// <summary>
        /// WebMvc
        /// </summary>
        [Description("WebMvc")]
        WebMvc = 2,
    }
}
