using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Models.Tables
{
    /// <summary>
    /// 分页方法返回值
    /// </summary>
    public class PageResult<TData>
    {
        /// <summary>
        /// 总数据量
        /// </summary>
        public int TotalRecords { get; set; }
        /// <summary>
        /// 查询到的数据
        /// </summary>
        public TData? Items { get; set; }
    }
}
