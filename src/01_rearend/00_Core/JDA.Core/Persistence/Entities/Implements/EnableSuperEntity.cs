﻿using JDA.Core.Attributes;
using JDA.Core.Persistence.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Entities
{
    /// <summary>
    /// 含有启用禁用属性的实体
    /// </summary>
    public partial class EnableSuperEntity : SuperEntity, IEnableSuperEntity
    {
        /// <summary>
        /// 是否启用（关联字段表）
        /// </summary>
        [Display(Name = "是否启用", Order = 900)]
        [ColumnMetadata(Name = "是否启用", Order = 900, IsDic = true, DicKey = "Enable")]
        public long Enabled { get; set; }
    }
}
