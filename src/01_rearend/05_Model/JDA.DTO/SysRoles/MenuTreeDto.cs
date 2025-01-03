﻿using JDA.Core.Attributes;
using JDA.Core.Dtos.Base;
using JDA.DTO.SysUsers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.DTO.SysRoles
{
    /// <summary>
    /// 菜单树节点dto
    /// </summary>
    public class MenuTreeDto : BaseTreeNodeDto<MenuTreeDto>
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
        /// 是否选中（true-是 false-不是）
        /// </summary>
        [Display(Name = "是否选中（true-是 false-不是）", Order = 2)]
        [ColumnMetadata(Name = "是否选中（true-是 false-不是）", Order = 2)]
        public bool IsCheck { get; set; }
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
        /// <summary>
        /// 该菜单下的按钮
        /// </summary>
        [Display(Name = "该菜单下的按钮", Order = 2)]
        [ColumnMetadata(Name = "该菜单下的按钮", Order = 2)]
        public List<ActionTreeDto> Actions { get; set; }
    }
}