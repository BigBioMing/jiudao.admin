using JDA.Core.Persistence.Contexts;
using JDA.Core.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JDA.IRepository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : SuperEntity
    {
        public JDABaseDbContext DbContext { get; }


        #region Queryable
        /// <summary>
        /// 查询主体（过滤掉了被删除的数据）
        /// </summary>
        IQueryable<TEntity> Queryable { get; }
        /// <summary>
        /// 查询主体（包含被删除的数据）
        /// </summary>
        IQueryable<TEntity> QueryableAll { get; }
        #endregion
    }
}
