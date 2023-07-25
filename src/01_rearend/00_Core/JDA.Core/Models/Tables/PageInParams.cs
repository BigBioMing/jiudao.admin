using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Models.Tables
{
    /// <summary>
    /// 分页方法入参
    /// </summary>
    public class PageInParams
    {
        public PageInParams(int pageIndex = 1, int pageSize = 10)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        /// <summary>
        /// 要跳转到的页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
    }
}
