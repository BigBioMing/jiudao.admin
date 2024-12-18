using JDA.Core.Attributes;
using JDA.Core.Views.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Model.Sys.SysRoles
{
    /// <summary>
    /// 保存用户信息模型
    /// </summary>
    public class SysRoleSaveVO : BaseSugerEntityViewModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键", Order = 1000)]
        public long Id { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [Display(Name = "编码", Order = 1)]
        [Required(ErrorMessage = "{0}是必填项")]
        public required string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称", Order = 2)]
        [Required(ErrorMessage = "{0}是必填项")]
        public required string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述", Order = 3)]
        public string? Description { get; set; }
    }
}
