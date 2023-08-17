using JDA.Core.Attributes;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JDA.Model.Sys.SysDictionaryDatas
{
    /// <summary>
    /// 字典项数据
    /// </summary>
    public class SysDictionaryDataTreeNodeVO
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
        /// 字典定义表ID
        /// </summary>
        [Display(Name = "字典定义表ID", Order = 3)]
        [ColumnMetadata(Name = "字典定义表ID", Order = 3)]
        public long DictionaryDefineId { get; set; }
        /// <summary>
        /// 上级机构ID
        /// </summary>
        [Display(Name = "上级机构ID", Order = 4)]
        [ColumnMetadata(Name = "上级机构ID", Order = 4)]
        public long ParentId { get; set; }
        /// <summary>
        /// 序号 越大越靠前
        /// </summary>
        [Display(Name = "序号 越大越靠前", Order = 5)]
        [ColumnMetadata(Name = "序号 越大越靠前", Order = 5)]
        public int Sort { get; set; }
        /// <summary>
        /// 字典项数据集合
        /// </summary>
        /// </summary>
        [Display(Name = "字典项数据集合", Order = 100)]
        [ColumnMetadata(Name = "字典项数据集合", Order = 100, Hidden = true)]
        public List<SysDictionaryDataTreeNodeVO> Childrens { get; set; }
    }
}
