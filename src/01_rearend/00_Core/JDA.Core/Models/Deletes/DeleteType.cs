using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Models.Deletes
{
    /// <summary>
    /// 删除类型
    /// </summary>
    public enum DeleteType
    {
        /// <summary>
        /// 逻辑删除
        /// </summary>
        [Description("逻辑删除")]
        Logical = 0,
        /// <summary>
        /// 物理删除
        /// </summary>
        [Description("物理删除")]
        Physical = 1
    }
}
