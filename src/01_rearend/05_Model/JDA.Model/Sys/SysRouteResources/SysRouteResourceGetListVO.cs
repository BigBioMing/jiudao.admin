using JDA.Core.Attributes;
using JDA.Core.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Model.Sys.SysRouteResources
{
    /// <summary>
    /// 查询路由资源列表模型
    /// </summary>
    public class SysRouteResourceGetListVO : PageViewModel
    {
        /// <summary>
        /// 路由编码
        /// </summary>
        [Display(Name = "路由编码", Order = 1)]
        [ColumnMetadata(Name = "路由编码", Order = 1)]
        public string? Code { get; set; }
        /// <summary>
        /// 路由名称
        /// </summary>
        [Display(Name = "路由名称", Order = 2)]
        [ColumnMetadata(Name = "路由名称", Order = 2)]
        public string? Name { get; set; }
        /// <summary>
        /// 路由标题
        /// </summary>
        [Display(Name = "路由标题", Order = 3)]
        [ColumnMetadata(Name = "路由标题", Order = 3)]
        public string? Title { get; set; }
    }
}
