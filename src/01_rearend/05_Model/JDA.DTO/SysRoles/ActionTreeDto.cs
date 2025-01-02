using JDA.Core.Attributes;
using JDA.Core.Dtos.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.DTO.SysRoles
{
    public class ActionTreeDto : BaseTreeNodeDto<ActionTreeDto>
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
        /// 是否选中（true-是 false-不是）
        /// </summary>
        [Display(Name = "是否选中（true-是 false-不是）", Order = 2)]
        [ColumnMetadata(Name = "是否选中（true-是 false-不是）", Order = 2)]
        public bool IsCheck { get; set; }
        /// <summary>
        /// 顺序（正序排序）
        /// </summary>
        [Display(Name = "顺序（正序排序）", Order = 2)]
        [ColumnMetadata(Name = "顺序（正序排序）", Order = 2)]
        public short Sort { get; set; }
    }
}
