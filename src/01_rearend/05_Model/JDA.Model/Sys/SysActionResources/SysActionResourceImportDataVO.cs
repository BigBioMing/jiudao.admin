using JDA.Core.Attributes;
using JDA.Core.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Model.Sys.SysActionResources
{
    /// <summary>
    /// 操作资源数据导出模型
    /// </summary>
    public class SysActionResourceImportDataVO : ImportDataViewModel
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Display(Name = "编码", Order = 1)]
        [ColumnMetadata(Name = "编码", Order = 1)]
        public string? Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称", Order = 2)]
        [ColumnMetadata(Name = "名称", Order = 2)]
        public string? Name { get; set; }
        /// <summary>
        /// 所属菜单Id
        /// </summary>
        [Display(Name = "所属菜单Id", Order = 2)]
        [ColumnMetadata(Name = "所属菜单Id", Order = 2)]
        public long? RouteResourceId { get; set; }
    }
}
