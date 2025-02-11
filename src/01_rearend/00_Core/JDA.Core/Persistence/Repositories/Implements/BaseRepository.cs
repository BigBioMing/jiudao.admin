﻿using AutoMapper.Execution;
using AutoMapper.Internal;
using JDA.Core.Models.Deletes;
using JDA.Core.Models.Operations;
using JDA.Core.Models.OrderBys;
using JDA.Core.Models.Tables;
using JDA.Core.Persistence.Contexts;
using JDA.Core.Persistence.Entities.Abstractions;
using JDA.Core.Users.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;

namespace JDA.Core.Persistence.Repositories.Implements
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public class BaseRepository<TEntity, TDbContext> where TEntity : class, ISuperEntity where TDbContext : JDABaseDbContext
    {
        private readonly TDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSetEntity;
        protected readonly ICurrentRunningContext _currentRunningContext;
        public TDbContext DbContext { get => _dbContext; }

        public BaseRepository(TDbContext dbContext, ICurrentRunningContext currentRunningContext)
        {
            this._dbContext = dbContext;
            this._dbSetEntity = this._dbContext.Set<TEntity>();
            this._currentRunningContext = currentRunningContext;
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
        /// <summary>
        /// 查询主体（过滤掉了被删除的数据）
        /// </summary>
        public virtual IQueryable<TEntity> QueryableNoTracking { get => this._dbSetEntity.AsQueryable().AsNoTracking().Where(n => n.IsDeleted == 0); }
        /// <summary>
        /// 查询主体（包含被删除的数据）
        /// </summary>
        public virtual IQueryable<TEntity> QueryableAllNoTracking { get => this._dbSetEntity.AsQueryable().AsNoTracking(); }
        #endregion

        #region 同步方法
        #region 查询单条数据
        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefault() => this.Queryable.FirstOrDefault();
        /// <summary>
        /// 根据条件查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate) => this.Queryable.FirstOrDefault(predicate);
        /// <summary>
        /// 根据ID排序后正序查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefaultOrderByAscById() => this.FirstOrDefaultOrderByAsc(n => n.Id);
        /// <summary>
        /// 根据ID排序后倒序查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefaultOrderByDescById() => this.FirstOrDefaultOrderByDesc(n => n.Id);
        /// <summary>
        /// 根据条件筛选后以ID排序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefaultOrderByAscById(Expression<Func<TEntity, bool>> predicate) => this.FirstOrDefaultOrderByAsc(predicate, n => n.Id);
        /// <summary>
        /// 根据条件筛选后以ID排序倒序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefaultOrderByDescById(Expression<Func<TEntity, bool>> predicate) => this.FirstOrDefaultOrderByDesc(predicate, n => n.Id);
        /// <summary>
        /// 根据指定字段排序后正序查询单条数据
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>

        public virtual TEntity? FirstOrDefaultOrderByAsc<TKey>(Expression<Func<TEntity, TKey>> keySelector) => this.Queryable.OrderBy(keySelector).FirstOrDefault();
        /// <summary>
        /// 根据指定字段排序后倒序查询单条数据
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefaultOrderByDesc<TKey>(Expression<Func<TEntity, TKey>> keySelector) => this.Queryable.OrderByDescending(keySelector).FirstOrDefault();
        /// <summary>
        /// 根据条件筛选后以指定字段排序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefaultOrderByAsc<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector) => this.Queryable.Where(predicate).OrderBy(keySelector).FirstOrDefault();
        /// <summary>
        /// 根据条件筛选后以指定字段倒序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefaultOrderByDesc<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector) => this.Queryable.Where(predicate).OrderByDescending(keySelector).FirstOrDefault();
        #endregion

        #region 查询集合数据
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public virtual List<TEntity> GetEntities() => this.Queryable.ToList();
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public virtual List<TEntity> GetEntities(Expression<Func<TEntity, bool>>? predicate) => predicate is null ? this.Queryable.ToList() : this.Queryable.Where(predicate).ToList();
        #endregion

        #region 查询分页数据
        /// <summary>
        /// 分页查询（根据ID倒序）
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <returns>返回查询到的数据</returns>
        public virtual PageResult<List<TEntity>> GetPageEntities(PageInParams pageInParams)
        {
            return GetPageEntities(pageInParams, null, OrderByType.Desc, n => n.Id);
        }

        /// <summary>
        /// 分页查询（根据ID倒序）
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <param name="wherePredicate">查询条件</param>
        /// <returns>返回查询到的数据</returns>
        public virtual PageResult<List<TEntity>> GetPageEntities(PageInParams pageInParams, Expression<Func<TEntity, bool>>? wherePredicate)
        {
            return GetPageEntities(pageInParams, wherePredicate, OrderByType.Desc, n => n.Id);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <param name="wherePredicate">查询条件</param>
        /// <param name="orderByType">排序方式</param>
        /// <param name="orderByKeySelector">排序字段</param>
        /// <returns>返回查询到的数据</returns>
        public virtual PageResult<List<TEntity>> GetPageEntities<TKey>(PageInParams pageInParams, Expression<Func<TEntity, bool>>? wherePredicate, OrderByType orderByType, Expression<Func<TEntity, TKey>> orderByKeySelector)
        {
            List<TEntity> list = new List<TEntity>();
            IQueryable<TEntity> query = this.Queryable;
            if (wherePredicate is null) query = this.Queryable;
            else query = query.Where(wherePredicate);

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

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">待添加的实体</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult<TEntity> Insert(TEntity entity)
        {
            entity.Id = 0;
            entity.IsDeleted = 0;
            entity.CreateId = _currentRunningContext.UserId;
            entity.CreateSource = _currentRunningContext.UserName;
            entity.CreateDate = DateTime.Now;
            entity.UpdateId = _currentRunningContext.UserId;
            entity.UpdateSource = _currentRunningContext.UserName;
            entity.UpdateDate = DateTime.Now;

            this._dbContext.Add(entity);
            this._dbContext.SaveChanges();

            return OperationResult<TEntity>.Success(entity);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entities">待添加的实体集合</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult<List<TEntity>> Insert(List<TEntity> entities)
        {

            DateTime now = DateTime.Now;
            foreach (TEntity entity in entities)
            {
                entity.Id = 0;
                entity.IsDeleted = 0;
                entity.CreateId = _currentRunningContext.UserId;
                entity.CreateSource = _currentRunningContext.UserName;
                entity.CreateDate = now;
                entity.UpdateId = _currentRunningContext.UserId;
                entity.UpdateSource = _currentRunningContext.UserName;
                entity.UpdateDate = now;
            }

            this._dbContext.Add(entities);
            this._dbContext.SaveChanges();

            return OperationResult<List<TEntity>>.Success(entities);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">待更新的实体</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult<TEntity> Update(TEntity entity)
        {
            entity.UpdateId = _currentRunningContext.UserId;
            entity.UpdateSource = _currentRunningContext.UserName;
            entity.UpdateDate = DateTime.Now;

            this._dbContext.Update(entity);
            this._dbContext.SaveChanges();

            return OperationResult<TEntity>.Success(entity);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities">待更新的实体集合</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult<List<TEntity>> Update(List<TEntity> entities)
        {

            DateTime now = DateTime.Now;
            foreach (TEntity entity in entities)
            {
                entity.UpdateId = _currentRunningContext.UserId;
                entity.UpdateSource = _currentRunningContext.UserName;
                entity.UpdateDate = DateTime.Now;
            }

            this._dbContext.Update(entities);
            this._dbContext.SaveChanges();

            return OperationResult<List<TEntity>>.Success(entities);
        }
        #endregion

        #region 启用/禁用
        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <param name="entity">待启用/禁用的实体</param>
        /// <param name="setEnableValue">要设置的值</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult Enable<TEnableEntity>(TEnableEntity entity, long setEnableValue) where TEnableEntity : IEnableSuperEntity, TEntity
        {
            entity.Enabled = setEnableValue;
            this.Update(entity);

            return OperationResult.Success();
        }
        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <param name="entities">待启用/禁用的实体集合</param>
        /// <param name="setEnableValue">要设置的值</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult Enable<TEnableEntity>(List<TEnableEntity> entities, long setEnableValue) where TEnableEntity : IEnableSuperEntity, TEntity
        {
            foreach (TEnableEntity entity in entities)
            {
                entity.Enabled = setEnableValue;
            }

            this.Update(entities as List<TEntity>);
            return OperationResult.Success();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">待删除的实体</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult Delete(TEntity entity, DeleteType deleteType = DeleteType.Logical)
        {
            if (deleteType == DeleteType.Physical)
            {
                this._dbContext.Remove(entity);
                this._dbContext.SaveChanges();
                return OperationResult.Success();
            }
            else
            {
                entity.IsDeleted = 1;
                entity.UpdateId = _currentRunningContext.UserId;
                entity.UpdateSource = _currentRunningContext.UserName;
                entity.UpdateDate = DateTime.Now;

                this._dbContext.Update(entity);
                this._dbContext.SaveChanges();
                return OperationResult.Success();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entities">待删除的实体集合</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult Delete(List<TEntity> entities, DeleteType deleteType = DeleteType.Logical)
        {
            if (deleteType == DeleteType.Physical)
            {
                this._dbContext.Remove(entities);
                this._dbContext.SaveChanges();
                return OperationResult.Success();
            }
            else
            {
                DateTime now = DateTime.Now;
                foreach (TEntity entity in entities)
                {
                    entity.IsDeleted = 1;
                    entity.UpdateId = _currentRunningContext.UserId;
                    entity.UpdateSource = _currentRunningContext.UserName;
                    entity.UpdateDate = DateTime.Now;
                }

                this._dbContext.Update(entities);
                this._dbContext.SaveChanges();
                return OperationResult.Success();
            }
        }
        #endregion
        #endregion

        #region 异步方法
        #region 查询单条数据
        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultAsync() => await this.Queryable.FirstOrDefaultAsync();
        /// <summary>
        /// 根据条件查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await this.Queryable.FirstOrDefaultAsync(predicate);
        /// <summary>
        /// 根据ID排序后正序查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultOrderByAscByIdAsync() => await this.FirstOrDefaultOrderByAscAsync(n => n.Id);
        /// <summary>
        /// 根据ID排序后倒序查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultOrderByDescByIdAsync() => await this.FirstOrDefaultOrderByDescAsync(n => n.Id);
        /// <summary>
        /// 根据条件筛选后以ID排序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultOrderByAscByIdAsync(Expression<Func<TEntity, bool>> predicate) => await this.FirstOrDefaultOrderByAscAsync(predicate, n => n.Id);
        /// <summary>
        /// 根据条件筛选后以ID排序倒序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultOrderByDescByIdAsync(Expression<Func<TEntity, bool>> predicate) => await this.FirstOrDefaultOrderByDescAsync(predicate, n => n.Id);
        /// <summary>
        /// 根据指定字段排序后正序查询单条数据
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>

        public virtual async Task<TEntity> FirstOrDefaultOrderByAscAsync<TKey>(Expression<Func<TEntity, TKey>> keySelector) => await this.Queryable.OrderBy(keySelector).FirstOrDefaultAsync();
        /// <summary>
        /// 根据指定字段排序后倒序查询单条数据
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultOrderByDescAsync<TKey>(Expression<Func<TEntity, TKey>> keySelector) => await this.Queryable.OrderByDescending(keySelector).FirstOrDefaultAsync();
        /// <summary>
        /// 根据条件筛选后以指定字段排序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultOrderByAscAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector) => await this.Queryable.Where(predicate).OrderBy(keySelector).FirstOrDefaultAsync();
        /// <summary>
        /// 根据条件筛选后以指定字段倒序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultOrderByDescAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector) => await this.Queryable.Where(predicate).OrderByDescending(keySelector).FirstOrDefaultAsync();
        #endregion

        #region 查询集合数据
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetEntitiesAsync() => await this.Queryable.ToListAsync();
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>>? predicate) => predicate is null ? await this.Queryable.ToListAsync() : await this.Queryable.Where(predicate).ToListAsync();
        #endregion

        #region 查询分页数据
        /// <summary>
        /// 分页查询（根据ID倒序）
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <returns>返回查询到的数据</returns>
        public virtual async Task<PageResult<List<TEntity>>> GetPageEntitiesAsync(PageInParams pageInParams)
        {
            return await GetPageEntitiesAsync(pageInParams, null, OrderByType.Desc, n => n.Id);
        }

        /// <summary>
        /// 分页查询（根据ID倒序）
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <param name="wherePredicate">查询条件</param>
        /// <returns>返回查询到的数据</returns>
        public virtual async Task<PageResult<List<TEntity>>> GetPageEntitiesAsync(PageInParams pageInParams, Expression<Func<TEntity, bool>>? wherePredicate)
        {
            return await GetPageEntitiesAsync(pageInParams, wherePredicate, OrderByType.Desc, n => n.Id);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <param name="wherePredicate">查询条件</param>
        /// <param name="orderByType">排序方式</param>
        /// <param name="orderByKeySelector">排序字段</param>
        /// <returns>返回查询到的数据</returns>
        public virtual async Task<PageResult<List<TEntity>>> GetPageEntitiesAsync<TKey>(PageInParams pageInParams, Expression<Func<TEntity, bool>>? wherePredicate, OrderByType orderByType, Expression<Func<TEntity, TKey>> orderByKeySelector)
        {
            List<TEntity> list = new List<TEntity>();
            IQueryable<TEntity> query = this.Queryable;
            if (wherePredicate is null) query = this.Queryable;
            else query = query.Where(wherePredicate);

            int totalCount = query.Count();
            if (totalCount > 0)
            {
                if (orderByType == OrderByType.Asc)
                    list = await query.OrderBy(orderByKeySelector).Skip((pageInParams.PageIndex - 1) * pageInParams.PageSize).Take(pageInParams.PageSize).ToListAsync();
                else
                    list = await query.OrderByDescending(orderByKeySelector).Skip((pageInParams.PageIndex - 1) * pageInParams.PageSize).Take(pageInParams.PageSize).ToListAsync();
            }

            return new PageResult<List<TEntity>>()
            {
                TotalRecords = totalCount,
                Items = list
            };
        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">待添加的实体</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult<TEntity>> InsertAsync(TEntity entity)
        {
            entity.Id = 0;
            entity.IsDeleted = 0;
            entity.CreateId = _currentRunningContext.UserId;
            entity.CreateSource = _currentRunningContext.UserName;
            entity.CreateDate = DateTime.Now;
            entity.UpdateId = _currentRunningContext.UserId;
            entity.UpdateSource = _currentRunningContext.UserName;
            entity.UpdateDate = DateTime.Now;

            await this._dbContext.AddAsync(entity);
            await this._dbContext.SaveChangesAsync();

            return OperationResult<TEntity>.Success(entity);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entities">待添加的实体集合</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult<List<TEntity>>> InsertAsync(List<TEntity> entities)
        {

            DateTime now = DateTime.Now;
            foreach (TEntity entity in entities)
            {
                entity.Id = 0;
                entity.IsDeleted = 0;
                entity.CreateId = _currentRunningContext.UserId;
                entity.CreateSource = _currentRunningContext.UserName;
                entity.CreateDate = now;
                entity.UpdateId = _currentRunningContext.UserId;
                entity.UpdateSource = _currentRunningContext.UserName;
                entity.UpdateDate = now;
            }

            await this._dbContext.AddRangeAsync(entities);
            await this._dbContext.SaveChangesAsync();

            return OperationResult<List<TEntity>>.Success(entities);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新（只更新指定字段）
        /// </summary>
        /// <param name="entity">待更新的实体</param>
        /// <param name="keySelector">需要更新的字段</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult<TEntity>> UpdateAsync<TKey>(TEntity entity, Expression<Func<TEntity, TKey>> keySelector)
        {
            this._dbContext.Attach(entity);
            var dbSetEntry = this._dbContext.Entry<TEntity>(entity);
            ParameterExpression parameterExpr = Expression.Parameter(typeof(TEntity), keySelector.Parameters[0].Name);
            if (keySelector.Body.NodeType == ExpressionType.New)
            {
                var a = (NewExpression)keySelector.Body;
                foreach (var p in a.Members)
                {
                    dbSetEntry.Property(p.Name).IsModified = true;
                }
            }
            else if (keySelector.Body.NodeType == ExpressionType.MemberAccess)
            {
                var a = (MemberExpression)keySelector.Body;
                dbSetEntry.Property(a.Member.Name).IsModified = true;
            }


            //entity.UpdateId = Random.Shared.NextInt64();
            entity.UpdateId = _currentRunningContext.UserId;
            entity.UpdateSource = _currentRunningContext.UserName;
            entity.UpdateDate = DateTime.Now;

            dbSetEntry.Property(n => n.UpdateId).IsModified = true;
            dbSetEntry.Property(n => n.UpdateSource).IsModified = true;
            dbSetEntry.Property(n => n.UpdateDate).IsModified = true;

            //this._dbContext.Update(entity);
            await this._dbContext.SaveChangesAsync();

            return OperationResult<TEntity>.Success(entity);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">待更新的实体</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult<TEntity>> UpdateAsync(TEntity entity)
        {
            entity.UpdateId = _currentRunningContext.UserId;
            entity.UpdateSource = _currentRunningContext.UserName;
            entity.UpdateDate = DateTime.Now;

            this._dbContext.Update(entity);
            await this._dbContext.SaveChangesAsync();

            return OperationResult<TEntity>.Success(entity);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities">待更新的实体集合</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult<List<TEntity>>> UpdateAsync(List<TEntity> entities)
        {

            DateTime now = DateTime.Now;
            foreach (TEntity entity in entities)
            {
                entity.UpdateId = _currentRunningContext.UserId;
                entity.UpdateSource = _currentRunningContext.UserName;
                entity.UpdateDate = DateTime.Now;
            }

            this._dbContext.UpdateRange(entities);
            await this._dbContext.SaveChangesAsync();

            return OperationResult<List<TEntity>>.Success(entities);
        }
        #endregion

        #region 启用/禁用
        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <param name="entity">待启用/禁用的实体</param>
        /// <param name="setEnableValue">要设置的值</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult> EnableAsync<TEnableEntity>(TEnableEntity entity, long setEnableValue) where TEnableEntity : IEnableSuperEntity, TEntity
        {
            entity.Enabled = setEnableValue;
            await this.UpdateAsync(entity);
            return OperationResult.Success();
        }
        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <param name="entities">待启用/禁用的实体集合</param>
        /// <param name="setEnableValue">要设置的值</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult> EnableAsync<TEnableEntity>(List<TEnableEntity> entities, long setEnableValue) where TEnableEntity : IEnableSuperEntity, TEntity
        {
            foreach (TEnableEntity entity in entities)
            {
                entity.Enabled = setEnableValue;
            }

            await this.UpdateAsync(entities as List<TEntity>);
            return OperationResult.Success();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">待删除的实体</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult> DeleteAsync(TEntity entity, DeleteType deleteType = DeleteType.Logical)
        {
            if (deleteType == DeleteType.Physical)
            {
                this._dbContext.Remove(entity);
                await this._dbContext.SaveChangesAsync();
                return OperationResult.Success();
            }
            else
            {
                entity.IsDeleted = 1;
                entity.UpdateId = _currentRunningContext.UserId;
                entity.UpdateSource = _currentRunningContext.UserName;
                entity.UpdateDate = DateTime.Now;

                this._dbContext.Update(entity);
                await this._dbContext.SaveChangesAsync();
                return OperationResult.Success();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entities">待删除的实体集合</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult> DeleteAsync(List<TEntity> entities, DeleteType deleteType = DeleteType.Logical)
        {
            if (deleteType == DeleteType.Physical)
            {
                this._dbContext.Remove(entities);
                await this._dbContext.SaveChangesAsync();
                return OperationResult.Success();
            }
            else
            {
                DateTime now = DateTime.Now;
                foreach (TEntity entity in entities)
                {
                    entity.IsDeleted = 1;
                    entity.UpdateId = _currentRunningContext.UserId;
                    entity.UpdateSource = _currentRunningContext.UserName;
                    entity.UpdateDate = DateTime.Now;
                }

                this._dbContext.UpdateRange(entities);
                await this._dbContext.SaveChangesAsync();
                return OperationResult.Success();
            }
        }
        #endregion
        #endregion
    }
}
