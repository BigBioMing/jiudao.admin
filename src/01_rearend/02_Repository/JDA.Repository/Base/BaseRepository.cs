using JDA.Core.Models.OrderBys;
using JDA.Core.Models.Tables;
using JDA.Core.Persistence.Contexts;
using JDA.Core.Persistence.Entities;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Repository.Base
{
    public class BaseRepository<TEntity> where TEntity : SuperEntity
    {
        private readonly JDABaseDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSetEntity;
        public JDABaseDbContext DbContext { get => _dbContext; }

        public BaseRepository(JDABaseDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSetEntity = this._dbContext.Set<TEntity>();
        }

        #region Queryable
        /// <summary>
        /// 查询主体（过滤掉了被删除的数据）
        /// </summary>
        public virtual IQueryable<TEntity> Queryable { get => this._dbSetEntity.AsQueryable().Where(n => n.IsDeleted == 0); }
        /// <summary>
        /// 查询主体（包含被删除的数据）
        /// </summary>
        public virtual IQueryable<TEntity> QueryableAll { get => this._dbSetEntity.AsQueryable(); }
        #endregion

        #region 查询单条数据
        public virtual TEntity? FirstOrDefault() => this.Queryable.FirstOrDefault();
        public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate) => this.Queryable.FirstOrDefault(predicate);
        public virtual TEntity? FirstOrDefaultOrderByAscById() => this.FirstOrDefaultOrderByAsc(n => n.Id);
        public virtual TEntity? FirstOrDefaultOrderByDescById() => this.FirstOrDefaultOrderByDesc(n => n.Id);
        public virtual TEntity? FirstOrDefaultOrderByAscById(Expression<Func<TEntity, bool>> predicate) => this.FirstOrDefaultOrderByAsc(predicate, n => n.Id);
        public virtual TEntity? FirstOrDefaultOrderByDescById(Expression<Func<TEntity, bool>> predicate) => this.FirstOrDefaultOrderByDesc(predicate, n => n.Id);

        public virtual TEntity? FirstOrDefaultOrderByAsc<TKey>(Expression<Func<TEntity, TKey>> keySelector) => this.Queryable.OrderBy(keySelector).FirstOrDefault();
        public virtual TEntity? FirstOrDefaultOrderByDesc<TKey>(Expression<Func<TEntity, TKey>> keySelector) => this.Queryable.OrderByDescending(keySelector).FirstOrDefault();
        public virtual TEntity? FirstOrDefaultOrderByAsc<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector) => this.Queryable.Where(predicate).OrderBy(keySelector).FirstOrDefault();
        public virtual TEntity? FirstOrDefaultOrderByDesc<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector) => this.Queryable.Where(predicate).OrderByDescending(keySelector).FirstOrDefault();
        #endregion

        #region 查询集合数据
        public virtual IQueryable<TEntity> GetEntities() => this.Queryable;
        public virtual IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate) => this.Queryable.Where(predicate);
        #endregion

        #region 查询分页数据
        public virtual PageResult<List<TEntity>> GetPageEntities(PageInParams pageInParams, Expression<Func<TEntity, bool>> wherePredicate)
        {
            List<TEntity> list = new List<TEntity>();
            IQueryable<TEntity> query = this.Queryable.Where(wherePredicate);
            int totalCount = query.Count();
            if (totalCount > 0)
            {
                list = query.OrderByDescending(n => n.Id).Skip((pageInParams.PageIndex - 1) * pageInParams.PageSize).Take(pageInParams.PageSize).ToList();
            }

            return new PageResult<List<TEntity>>()
            {
                TotalRecords = totalCount,
                Items = list
            };
        }
        public virtual PageResult<List<TEntity>> GetPageEntities<TKey>(PageInParams pageInParams, Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, TKey>> orderByKeySelector)
        {
            List<TEntity> list = new List<TEntity>();
            IQueryable<TEntity> query = this.Queryable.Where(wherePredicate);
            int totalCount = query.Count();
            if (totalCount > 0)
            {
                list = query.OrderByDescending(orderByKeySelector).Skip((pageInParams.PageIndex - 1) * pageInParams.PageSize).Take(pageInParams.PageSize).ToList();
            }

            return new PageResult<List<TEntity>>()
            {
                TotalRecords = totalCount,
                Items = list
            };
        }
        public virtual PageResult<List<TEntity>> GetPageEntities<TKey>(PageInParams pageInParams, Expression<Func<TEntity, bool>> wherePredicate, OrderByType orderByType, Expression<Func<TEntity, TKey>> orderByKeySelector)
        {
            List<TEntity> list = new List<TEntity>();
            IQueryable<TEntity> query = this.Queryable.Where(wherePredicate);
            int totalCount = query.Count();
            if (totalCount > 0)
            {
                if (orderByType == OrderByType.Asc)
                    list = query.OrderBy(orderByKeySelector).Skip((pageInParams.PageIndex - 1) * pageInParams.PageSize).Take(pageInParams.PageSize).ToList();
                else
                    list = query.OrderByDescending(orderByKeySelector).Skip((pageInParams.PageIndex - 1) * pageInParams.PageSize).Take(pageInParams.PageSize).ToList();
            }

            return new PageResult<List<TEntity>>()
            {
                TotalRecords = totalCount,
                Items = list
            };
        }
        #endregion
    }
}
