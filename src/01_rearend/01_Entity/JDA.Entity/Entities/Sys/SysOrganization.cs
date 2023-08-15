using JDA.Core.Attributes;
using JDA.Core.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Entity.Entities.Sys
{
    /// <summary>
    /// 组织机构表
    /// </summary>
    public partial class SysOrganization : TreeNodeEnableSuperEntity
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
        /// 类型
        /// </summary>
        [Display(Name = "类型", Order = 5)]
        [ColumnMetadata(Name = "类型", Order = 5)]
        public long Type { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态", Order = 6)]
        [ColumnMetadata(Name = "状态", Order = 6)]
        public long Status { get; set; }
    }
}
