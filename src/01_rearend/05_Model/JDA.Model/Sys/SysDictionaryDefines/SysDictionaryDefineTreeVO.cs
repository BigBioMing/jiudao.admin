using JDA.Core.Attributes;
using JDA.Core.Views.ViewModels.Base;
using JDA.Model.Sys.SysDictionaryDatas;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JDA.Model.Sys.SysDictionaryDefines
{
    /// <summary>
    /// 字典定义信息
    /// </summary>
    public class SysDictionaryDefineTreeVO : BaseSugerEntityViewModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键", Order = 1000)]
        [ColumnMetadata(Name = "主键", Order = 1000, Hidden = true)]
        public long Id { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [Display(Name = "编码", Order = 1)]
        [ColumnMetadata(Name = "编码", Order = 1)]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称", Order = 2)]
        [ColumnMetadata(Name = "名称", Order = 2)]
        public string Name { get; set; }
        /// <summary>
        /// 字典项数据集合
        /// </summary>
        /// </summary>
        [Display(Name = "字典项数据集合", Order = 100)]
        [ColumnMetadata(Name = "字典项数据集合", Order = 100, Hidden = true)]
        public List<SysDictionaryDataTreeNodeVO> Childrens { get; set; }
    }
}
