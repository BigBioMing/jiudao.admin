using JDA.Core.Attributes;
using JDA.Core.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Model.Sys.SysUsers
{
    /// <summary>
    /// 查询用户信息模型
    /// </summary>
    public class SysUserGetListVO : PageViewModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Display(Name = "账号", Order = 1)]
        [ColumnMetadata(Name = "账号", Order = 1)]
        public string? Account { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称", Order = 2)]
        [ColumnMetadata(Name = "名称", Order = 2)]
        public string? Name { get; set; }
    }
}
