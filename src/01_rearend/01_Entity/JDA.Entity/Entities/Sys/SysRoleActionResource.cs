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
    /// 角色表与操作资源表的关系表
    /// </summary>
    public partial class SysRoleActionResource : SuperEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Display(Name = "角色ID", Order = 1)]
        [ColumnMetadata(Name = "角色ID", Order = 1)]
        public long RoleId { get; set; }
        /// <summary>
        /// 操作资源表ID
        /// </summary>
        [Display(Name = "操作资源表ID", Order = 2)]
        [ColumnMetadata(Name = "操作资源表ID", Order = 2)]
        public long ActionResourceId { get; set; }
    }
}
