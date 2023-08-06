using JDA.Core.Attributes;
using JDA.Core.Views.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Model.Sys.SysUsers
{
    /// <summary>
    /// 保存用户信息模型
    /// </summary>
    public class SysUserSaveVO : BaseSugerEntityViewModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键", Order = 1000)]
        [ColumnMetadata(Name = "主键", Order = 1000, Hidden = true)]
        public long Id { get; set; }
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
        /// <summary>
        /// 是否启用（关联字段表）
        /// </summary>
        [Display(Name = "是否启用", Order = 900)]
        [ColumnMetadata(Name = "是否启用", Order = 900, IsDic = true, DicKey = "Enable")]
        public long Enabled { get; set; }
        /// <summary>
        /// 组织机构ID
        /// </summary>
        [Display(Name = "组织机构ID", Order = 901)]
        [ColumnMetadata(Name = "组织机构ID", Order = 901)]
        public List<long> OrgIds { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        [Display(Name = "角色ID", Order = 902)]
        [ColumnMetadata(Name = "角色ID", Order = 902)]
        public List<long> RoleIds { get; set; }
    }
}
