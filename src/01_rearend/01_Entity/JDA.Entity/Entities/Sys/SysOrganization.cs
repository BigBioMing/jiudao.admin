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
        /// 类型
        /// </summary>
        [Display(Name = "类型", Order = 6)]
        [ColumnMetadata(Name = "类型", Order = 6)]
        public long Type { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态", Order = 7)]
        [ColumnMetadata(Name = "状态", Order = 7)]
        public long Status { get; set; }
    }
}
