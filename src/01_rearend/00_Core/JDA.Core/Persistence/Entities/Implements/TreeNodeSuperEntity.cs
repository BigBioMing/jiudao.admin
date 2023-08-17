using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDA.Core.Attributes;
using System.Xml.Linq;
using JDA.Core.Persistence.Entities.Abstractions;
using OfficeOpenXml.Style;

namespace JDA.Core.Persistence.Entities
{
    /// <summary>
    /// 树形表超类
    /// </summary>
    public class TreeNodeSuperEntity : SuperEntity, ITreeNodeSuperEntity
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
        /// 上级机构ID
        /// </summary>
        [Display(Name = "上级机构ID", Order = 3)]
        [ColumnMetadata(Name = "上级机构ID", Order = 3)]
        public long ParentId { get; set; }
        /// <summary>
        /// Path
        /// </summary>
        [Display(Name = "Path", Order = 4)]
        [ColumnMetadata(Name = "Path", Order = 4, Hidden = true)]
        public string Path { get; set; }
        /// <summary>
        /// Path
        /// </summary>
        [Display(Name = "Sort", Order = 5)]
        [ColumnMetadata(Name = "Sort", Order = 5, Hidden = true)]
        public int Sort { get; set; }
    }
}
