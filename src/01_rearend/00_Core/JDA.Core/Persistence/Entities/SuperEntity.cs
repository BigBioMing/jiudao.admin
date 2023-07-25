using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Entities
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public partial class SuperEntity<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public TKey Id { get; set; }
        /// <summary>
        /// 是否删除 0-正常 1-已删除
        /// </summary>
        public int IsDeleted { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        public long CreateId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateSource { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 修改人Id
        /// </summary>
        public long UpdateId { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateSource { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }

    /// <summary>
    /// 实体基类
    /// </summary>
    public partial class SuperEntity : SuperEntity<long>
    {

    }
}
