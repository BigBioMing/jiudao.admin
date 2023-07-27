using JDA.Core.Users.Distinguish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Users.Abstractions
{
    /// <summary>
    /// 应用程序信息
    /// </summary>
    public interface IApplicationInformation
    {
        /// <summary>
        /// 宿主类型
        /// </summary>
        public HostType HostType { get; }
        /// <summary>
        /// 操作系统类型
        /// </summary>
        public OSType OSType { get; }
        /// <summary>
        /// 应用程序类型
        /// </summary>
        public ApplicationType ApplicationType { get; }
        public string Protocol { get; set; }
        public string Host { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }
    }
}
