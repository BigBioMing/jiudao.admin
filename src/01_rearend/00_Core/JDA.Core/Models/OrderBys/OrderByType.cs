using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Models.OrderBys
{
    /// <summary>
    /// 排序方式
    /// </summary>
    public enum OrderByType
    {
        /// <summary>
        /// 正序
        /// </summary>
        [Description("正序")]
        Asc = 0,
        /// <summary>
        /// 倒序
        /// </summary>
        [Description("倒序")]
        Desc = 1
    }
}
