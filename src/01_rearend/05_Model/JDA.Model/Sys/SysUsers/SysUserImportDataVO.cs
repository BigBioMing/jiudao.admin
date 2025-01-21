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
    /// 用户数据导出模型
    /// </summary>
    public class SysUserImportDataVO : ImportDataViewModel
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
        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码", Order = 2)]
        [ColumnMetadata(Name = "手机号码", Order = 2)]
        public string? Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱", Order = 2)]
        [ColumnMetadata(Name = "邮箱", Order = 2)]
        public string? Email { get; set; }
    }
}
