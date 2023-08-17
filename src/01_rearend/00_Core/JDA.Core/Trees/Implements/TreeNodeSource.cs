using JDA.Core.Attributes;
using JDA.Core.Trees.Abstractions;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JDA.Core.Trees.Implements
{
    public class TreeNodeSource : ITreeNodeSource
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键", Order = 1000)]
        [ColumnMetadata(Name = "主键", Order = 1000, Hidden = true)]
        public long Id { get; set; }
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
