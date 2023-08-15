using JDA.Core.Attributes;
using JDA.Core.Persistence.Entities;
using JDA.Core.Persistence.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Entity.Entities.Sys
{
    /// <summary>
    /// 字典定义表
    /// </summary>
    public partial class SysDictionaryData : SuperEntity
    {
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
        /// 描述
        /// </summary>
        [Display(Name = "描述", Order = 6)]
        [ColumnMetadata(Name = "描述", Order = 6)]
        public string Description { get; set; }
        /// <summary>
        /// 是否启用 false-禁用 true-启用
        /// </summary>
        [Display(Name = "是否启用", Order = 7)]
        [ColumnMetadata(Name = "是否启用", Order = 7)]
        public bool Enabled { get; set; }
    }
}
