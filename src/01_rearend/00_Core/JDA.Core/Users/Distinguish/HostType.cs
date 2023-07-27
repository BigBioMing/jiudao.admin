using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Users.Distinguish
{
    /// <summary>
    /// 宿主类型
    /// </summary>
    public enum HostType
    {
        /// <summary>
        /// Windows
        /// </summary>
        [Description("Windows")]
        Windows = 0,
        /// <summary>
        /// Linux
        /// </summary>
        [Description("Linux")]
        Linux = 1,
        /// <summary>
        /// Docker
        /// </summary>
        [Description("Docker")]
        Docker = 2,
        /// <summary>
        /// K8s
        /// </summary>
        [Description("K8s")]
        K8s = 3
    }
}
