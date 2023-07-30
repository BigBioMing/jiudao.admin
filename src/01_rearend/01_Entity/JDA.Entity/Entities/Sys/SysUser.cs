using JDA.Core.Attributes;
using JDA.Core.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Entity.Entities.Sys
{
    /// <summary>
    /// 系统用户表
    /// </summary>
    public partial class SysUser : EnableSuperEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Display(Name = "账号", Order = 1)]
        [ColumnMetadata(Name = "账号", Order = 1)]
        public string Account { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称", Order = 2)]
        [ColumnMetadata(Name = "名称", Order = 2)]
        public string Name { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码", Order = 3)]
        [ColumnMetadata(Name = "手机号码", Order = 3, IsEncry = true, EncryType = "AES")]
        public string Mobile { get; set; }
        /// <summary>
        /// 性别（关联字段表）
        /// </summary>
        [Display(Name = "性别", Order = 4)]
        [ColumnMetadata(Name = "性别", Order = 4, IsDic = true, DicKey = "Gender")]
        public long Gender { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱", Order = 5)]
        [ColumnMetadata(Name = "邮箱", Order = 5)]
        public string Email { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码", Order = 6)]
        [ColumnMetadata(Name = "密码", Order = 6, Hidden = true)]
        public string Password { get; set; }
        /// <summary>
        /// 密码盐
        /// </summary>
        [Display(Name = "密码盐", Order = 7)]
        [ColumnMetadata(Name = "密码盐", Order = 7, Hidden = true)]
        public string PasswordSalt { get; set; }
    }
}
