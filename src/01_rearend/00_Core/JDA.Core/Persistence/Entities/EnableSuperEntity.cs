using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Entities
{
    /// <summary>
    /// 含有启用禁用属性的实体
    /// </summary>
    public partial class EnableSuperEntity : SuperEntity
    {
        /// <summary>
        /// 是否启用（关联字段表）
        /// </summary>
        public long Enabled { get; set; }
    }
}
