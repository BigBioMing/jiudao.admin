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
    /// 用户表与组织机构表的关系表
    /// </summary>
    public partial class SysUserOrganization : SuperEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID", Order = 1)]
        [ColumnMetadata(Name = "用户ID", Order = 1)]
        public long UserId { get; set; }
        /// <summary>
        /// 组织机构ID
        /// </summary>
        [Display(Name = "组织机构ID", Order = 2)]
        [ColumnMetadata(Name = "组织机构ID", Order = 2)]
        public long OrgId { get; set; }
    }
}
