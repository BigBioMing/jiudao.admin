using JDA.Core.Attributes;
using JDA.Core.Views.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Views.ViewModels
{
    /// <summary>
    /// 导出数据模型
    /// </summary>
    public class ImportDataViewModel : BaseViewModel
    {
        /// <summary>
        /// 选中的列
        /// </summary>
        [Display(Name = "选中的列", Order = 100)]
        [ColumnMetadata(Name = "选中的列", Order = 100)]
        public List<ImportDataColumnViewModel> CheckedColumns { get; set; }
    }
    /// <summary>
    /// 导出数据列模型
    /// </summary>
    public class ImportDataColumnViewModel : BaseViewModel
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        [Display(Name = "字段名称", Order = 1)]
        [Required(ErrorMessage = "{0}是必填项")]
        [ColumnMetadata(Name = "字段名称", Order = 2)]
        public string Key { get; set; }
        /// <summary>
        /// 字段注释
        /// </summary>
        [Display(Name = "字段注释", Order = 1)]
        [ColumnMetadata(Name = "字段注释", Order = 2)]
        public string Title { get; set; }
    }
}
