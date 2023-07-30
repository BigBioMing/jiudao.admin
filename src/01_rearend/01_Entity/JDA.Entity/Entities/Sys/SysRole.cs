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
    /// 角色表
    /// </summary>
    public partial class SysRole : EnableSuperEntity
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
        /// 描述
        /// </summary>
        [Display(Name = "描述", Order = 3)]
        [ColumnMetadata(Name = "描述", Order = 3)]
        public string Description { get; set; }
    }
}
