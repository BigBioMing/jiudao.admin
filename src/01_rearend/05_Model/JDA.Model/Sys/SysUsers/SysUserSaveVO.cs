using JDA.Core.Attributes;
using JDA.Core.Views.ViewModels;
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
        public long Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [Display(Name = "账号", Order = 1)]
        [Required(ErrorMessage = "{0}是必填项")]
        [Length(3, 20, ErrorMessage = "{0}长度必须在3到20之间")]
        public string Account { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称", Order = 2,Prompt ="hhhh")]
        [Required(ErrorMessage = "名称是必填项")]
        [Length(1, 5, ErrorMessage = "名称长度必须在1到5之间")]
        public string Name { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码", Order = 3)]
        public string Mobile { get; set; }
        /// <summary>
        /// 性别（关联字典表）
        /// </summary>
        [Display(Name = "性别", Order = 4)]
        public long? Gender { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱", Order = 5)]
        [Required(ErrorMessage = "{0}是必填项")]
        public string? Email { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码", Order = 6)]
        [Required(ErrorMessage = "{0}是必填项")]
        [Length(6, 20, ErrorMessage = "{0}长度必须在6到20之间")]
        public string Password { get; set; }
        /// <summary>
        /// 是否启用（关联字段表）
        /// </summary>
        [Display(Name = "是否启用", Order = 900)]
        public long? Enabled { get; set; }
        /// <summary>
        /// 组织机构ID
        /// </summary>
        [Display(Name = "组织机构ID", Order = 901)]
        public List<long>? OrgIds { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        [Display(Name = "角色ID", Order = 902)]
        public List<long>? RoleIds { get; set; }
    }
}
