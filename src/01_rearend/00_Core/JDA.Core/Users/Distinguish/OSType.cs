using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Users.Distinguish
{
    /// <summary>
    /// 操作系统类型
    /// </summary>
    public enum OSType
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
        Linux = 1
    }
}
