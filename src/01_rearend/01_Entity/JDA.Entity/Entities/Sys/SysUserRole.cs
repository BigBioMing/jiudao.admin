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
    /// 用户表与角色表的关系表
    /// </summary>
    public partial class SysUserRole : SuperEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID", Order = 1)]
        [ColumnMetadata(Name = "用户ID", Order = 1)]
        public long UserId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        [Display(Name = "角色ID", Order = 2)]
        [ColumnMetadata(Name = "角色ID", Order = 2)]
        public long RoleId { get; set; }
    }
}
