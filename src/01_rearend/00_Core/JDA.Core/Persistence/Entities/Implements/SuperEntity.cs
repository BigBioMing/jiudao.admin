using JDA.Core.Attributes;
using JDA.Core.Persistence.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Entities
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public partial class SuperEntity<TKey> : ISuperEntity<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键", Order = 1000)]
        [ColumnMetadata(Name = "主键", Order = 1000, Hidden = true)]
        public TKey Id { get; set; }
        /// <summary>
        /// 是否删除 0-正常 1-已删除
        /// </summary>
        [Display(Name = "是否删除", Order = 1001)]
        [ColumnMetadata(Name = "是否删除", Order = 1001, Hidden = true)]
        public int IsDeleted { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        [Display(Name = "创建人Id", Order = 1002)]
        [ColumnMetadata(Name = "创建人Id", Order = 1002, Hidden = true)]
        public long CreateId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人", Order = 1003)]
        [ColumnMetadata(Name = "创建人", Order = 1003)]
        public string CreateSource { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间", Order = 1004)]
        [ColumnMetadata(Name = "创建时间", Order = 1004, Format = "yyyy-MM-dd HH:mm:ss")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 修改人Id
        /// </summary>
        [Display(Name = "修改人Id", Order = 1005)]
        [ColumnMetadata(Name = "修改人Id", Order = 1005, Hidden = true)]
        public long UpdateId { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Display(Name = "修改人", Order = 1006)]
        [ColumnMetadata(Name = "修改人", Order = 1006, Hidden = true)]
        public string UpdateSource { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "修改时间", Order = 1007)]
        [ColumnMetadata(Name = "修改时间", Order = 1007, Format = "yyyy-MM-dd HH:mm:ss", Hidden = true)]
        public DateTime UpdateDate { get; set; }
    }

    /// <summary>
    /// 实体基类
    /// </summary>
    public partial class SuperEntity : SuperEntity<long>, ISuperEntity
    {

    }
}
