using JDA.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Dtos.Base
{
    public class BaseTreeNodeDto<T> : BaseDto where T : BaseTreeNodeDto<T>
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        [Display(Name = "父级Id", Order = 2)]
        [ColumnMetadata(Name = "父级Id", Order = 2)]
        public long ParentId { get; set; }
        /// <summary>
        /// 下级节点
        /// </summary>
        public List<T> Childrens { get; set; }
    }
}
