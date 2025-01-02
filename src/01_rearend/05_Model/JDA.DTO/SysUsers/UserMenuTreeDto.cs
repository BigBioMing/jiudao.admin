using JDA.Core.Attributes;
using JDA.Core.Dtos.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.DTO.SysUsers
{
    /// <summary>
    /// 用户菜单树节点dto
    /// </summary>
    public class UserMenuTreeDto : BaseTreeNodeDto<UserMenuTreeDto>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键", Order = 1000)]
        [ColumnMetadata(Name = "主键", Order = 1000, Hidden = true)]
        public long Id { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [Display(Name = "编码", Order = 1)]
        [ColumnMetadata(Name = "编码", Order = 1)]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称", Order = 2)]
        [ColumnMetadata(Name = "名称", Order = 2)]
        public string Name { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题", Order = 2)]
        [ColumnMetadata(Name = "标题", Order = 2)]
        public string Title { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        [Display(Name = "Url", Order = 2)]
        [ColumnMetadata(Name = "Url", Order = 2)]
        public string Url { get; set; }
        /// <summary>
        /// 重定向地址
        /// </summary>
        [Display(Name = "重定向地址", Order = 2)]
        [ColumnMetadata(Name = "重定向地址", Order = 2)]
        public string? Redirect { get; set; }
        /// <summary>
        /// 组件路径（当前路径对应的组件，或者默认组件：BasicLayout）
        /// </summary>
        [Display(Name = "组件路径", Order = 2)]
        [ColumnMetadata(Name = "组件路径", Order = 2)]
        public string Component { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [Display(Name = "图标", Order = 2)]
        [ColumnMetadata(Name = "图标", Order = 2)]
        public string Icon { get; set; }
        /// <summary>
        /// 是否在菜单中显示（true-显示 false-不显示）
        /// </summary>
        [Display(Name = "是否在菜单中显示（true-显示 false-不显示）", Order = 2)]
        [ColumnMetadata(Name = "是否在菜单中显示（true-显示 false-不显示）", Order = 2)]
        public bool ShowInMenu { get; set; }
        /// <summary>
        /// 是否是外部网页（true-是 false-不是）
        /// </summary>
        [Display(Name = "是否是外部网页（true-是 false-不是）", Order = 2)]
        [ColumnMetadata(Name = "是否是外部网页（true-是 false-不是）", Order = 2)]
        public bool IsThird { get; set; }
    }
}
