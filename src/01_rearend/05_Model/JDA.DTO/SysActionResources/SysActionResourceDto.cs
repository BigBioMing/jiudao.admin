using JDA.Core.Attributes;
using JDA.Core.Dtos.Base;
using JDA.DTO.SysUsers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.DTO.SysActionResources
{
    public class SysActionResourceDto : BaseDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键", Order = 1000)]
        [ColumnMetadata(Name = "主键", Order = 1000, Hidden = true)]
        public long? Id { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [Display(Name = "编码", Order = 1)]
        [ColumnMetadata(Name = "编码", Order = 1)]
        [Required(ErrorMessage = "{0}是必填项")]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称", Order = 2)]
        [ColumnMetadata(Name = "名称", Order = 2)]
        [Required(ErrorMessage = "{0}是必填项")]
        public string Name { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [Display(Name = "序号", Order = 2)]
        [ColumnMetadata(Name = "序号", Order = 2)]
        public short? Sort { get; set; }
    }
}
