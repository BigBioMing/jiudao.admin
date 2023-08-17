using JDA.Core.Mappers.Abstractions;
using JDA.Core.Models.Deletes;
using JDA.Core.Models.Operations;
using JDA.Core.Models.OrderBys;
using JDA.Core.Models.Tables;
using JDA.Core.Persistence.Contexts;
using JDA.Core.Persistence.Entities;
using JDA.Core.Persistence.Entities.Abstractions;
using JDA.Core.Persistence.Repositories.Abstractions;
using JDA.Core.Trees.Implements;
using JDA.Core.Trees.Loader;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Services.Implements
{
    /// <summary>
    /// Service基类
    /// </summary>
    public class BaseService<TEntity, TDbContext> where TEntity : class, ISuperEntity where TDbContext : JDABaseDbContext
    {
        /// <summary>
        /// 仓储
        /// </summary>
        protected readonly IBaseRepository<TEntity, TDbContext> _currentRepository;
        /// <summary>
        /// 对象映射转换
        /// </summary>
        protected readonly IShapeMapper _mapper;
        public BaseService(IShapeMapper mapper, IBaseRepository<TEntity, TDbContext> currentRepository)
        {
            this._mapper = mapper;
            this._currentRepository = currentRepository;
        }

        #region 同步方法
        #region 查询单条数据
        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefault() => this._currentRepository.FirstOrDefault();
        /// <summary>
        /// 根据条件查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate) => this._currentRepository.FirstOrDefault(predicate);
        /// <summary>
        /// 根据ID排序后正序查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefaultOrderByAscById() => this._currentRepository.FirstOrDefaultOrderByAscById();
        /// <summary>
        /// 根据ID排序后倒序查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefaultOrderByDescById() => this._currentRepository.FirstOrDefaultOrderByDescById();
        /// <summary>
        /// 根据条件筛选后以ID排序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefaultOrderByAscById(Expression<Func<TEntity, bool>> predicate) => this._currentRepository.FirstOrDefaultOrderByAscById(predicate);
        /// <summary>
        /// 根据条件筛选后以ID排序倒序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefaultOrderByDescById(Expression<Func<TEntity, bool>> predicate) => this._currentRepository.FirstOrDefaultOrderByDescById(predicate);
        /// <summary>
        /// 根据指定字段排序后正序查询单条数据
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>

        public virtual TEntity? FirstOrDefaultOrderByAsc<TKey>(Expression<Func<TEntity, TKey>> keySelector) => this._currentRepository.FirstOrDefaultOrderByAsc(keySelector);
        /// <summary>
        /// 根据指定字段排序后倒序查询单条数据
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefaultOrderByDesc<TKey>(Expression<Func<TEntity, TKey>> keySelector) => this._currentRepository.FirstOrDefaultOrderByDesc(keySelector);
        /// <summary>
        /// 根据条件筛选后以指定字段排序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefaultOrderByAsc<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector) => this._currentRepository.FirstOrDefaultOrderByAsc(predicate, keySelector);
        /// <summary>
        /// 根据条件筛选后以指定字段倒序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual TEntity? FirstOrDefaultOrderByDesc<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector) => this._currentRepository.FirstOrDefaultOrderByDesc(predicate, keySelector);
        #endregion

        #region 查询集合数据
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public virtual List<TEntity> GetEntities() => this._currentRepository.GetEntities();
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public virtual List<TEntity> GetEntities(Expression<Func<TEntity, bool>>? predicate) => this._currentRepository.GetEntities(predicate);
        #endregion

        #region 查询分页数据
        /// <summary>
        /// 分页查询（根据ID倒序）
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <returns>返回查询到的数据</returns>
        public virtual PageResult<List<TEntity>> GetPageEntities(PageInParams pageInParams) => this._currentRepository.GetPageEntities(pageInParams);

        /// <summary>
        /// 分页查询（根据ID倒序）
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <param name="wherePredicate">查询条件</param>
        /// <returns>返回查询到的数据</returns>
        public virtual PageResult<List<TEntity>> GetPageEntities(PageInParams pageInParams, Expression<Func<TEntity, bool>>? wherePredicate) => this._currentRepository.GetPageEntities(pageInParams, wherePredicate);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <param name="wherePredicate">查询条件</param>
        /// <param name="orderByType">排序方式</param>
        /// <param name="orderByKeySelector">排序字段</param>
        /// <returns>返回查询到的数据</returns>
        public virtual PageResult<List<TEntity>> GetPageEntities<TKey>(PageInParams pageInParams, Expression<Func<TEntity, bool>>? wherePredicate, OrderByType orderByType, Expression<Func<TEntity, TKey>> orderByKeySelector) => this._currentRepository.GetPageEntities(pageInParams, wherePredicate, orderByType, orderByKeySelector);
        #endregion

        #region 获取树状结构的数据
        /// <summary>
        /// 获取树状结构的数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sources">数据源</param>
        /// <param name="parentId">指定从哪一级开始获取</param>
        /// <returns></returns>
        public virtual List<TreeNode> GetTrees<TSource>(List<TSource> sources, long parentId = 0) where TSource : ITreeNodeSuperEntity
        {
            return TreeLoader.GetTrees<TSource>(sources, parentId);
        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">待添加的实体</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult<TEntity> Insert(TEntity entity) => this._currentRepository.Insert(entity);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entities">待添加的实体集合</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult<List<TEntity>> Insert(List<TEntity> entities) => this._currentRepository.Insert(entities);
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">待更新的实体</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult<TEntity> Update(TEntity entity) => this._currentRepository.Update(entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities">待更新的实体集合</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult<List<TEntity>> Update(List<TEntity> entities) => this._currentRepository.Update(entities);
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
            return this._currentRepository.Enable(entity, setEnableValue);
        }
        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <param name="entities">待启用/禁用的实体集合</param>
        /// <param name="setEnableValue">要设置的值</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult Enable<TEnableEntity>(List<TEnableEntity> entities, long setEnableValue) where TEnableEntity : IEnableSuperEntity, TEntity
        {
            return this._currentRepository.Enable(entities, setEnableValue);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">待删除的实体的ID</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult Delete(long id, DeleteType deleteType = DeleteType.Logical)
        {
            TEntity? entity = this._currentRepository.FirstOrDefault(n => n.Id == id);
            if (entity is null)
                return OperationResult.Success();

            return this.Delete(entity, deleteType);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">待删除的实体</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult Delete(TEntity entity, DeleteType deleteType = DeleteType.Logical) => this._currentRepository.Delete(entity, deleteType);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">待删除的实体的ID集合</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult Delete(List<long> ids, DeleteType deleteType = DeleteType.Logical)
        {
            List<TEntity> entities = this._currentRepository.GetEntities(n => ids.Contains(n.Id)).ToList();
            if (entities.Count == 0)
                return OperationResult.Success();

            return this.Delete(entities, deleteType);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entities">待删除的实体集合</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        public virtual OperationResult Delete(List<TEntity> entities, DeleteType deleteType = DeleteType.Logical) => this._currentRepository.Delete(entities, deleteType);
        #endregion
        #endregion

        #region 异步方法
        #region 查询单条数据
        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultAsync() => await this._currentRepository.FirstOrDefaultAsync();
        /// <summary>
        /// 根据条件查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await this._currentRepository.FirstOrDefaultAsync(predicate);
        /// <summary>
        /// 根据ID排序后正序查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultOrderByAscByIdAsync() => await this._currentRepository.FirstOrDefaultOrderByAscByIdAsync();
        /// <summary>
        /// 根据ID排序后倒序查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultOrderByDescByIdAsync() => await this._currentRepository.FirstOrDefaultOrderByDescByIdAsync();
        /// <summary>
        /// 根据条件筛选后以ID排序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultOrderByAscByIdAsync(Expression<Func<TEntity, bool>> predicate) => await this._currentRepository.FirstOrDefaultOrderByAscByIdAsync(predicate);
        /// <summary>
        /// 根据条件筛选后以ID排序倒序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultOrderByDescByIdAsync(Expression<Func<TEntity, bool>> predicate) => await this._currentRepository.FirstOrDefaultOrderByDescByIdAsync(predicate);
        /// <summary>
        /// 根据指定字段排序后正序查询单条数据
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>

        public virtual async Task<TEntity> FirstOrDefaultOrderByAscAsync<TKey>(Expression<Func<TEntity, TKey>> keySelector) => await this._currentRepository.FirstOrDefaultOrderByAscAsync(keySelector);
        /// <summary>
        /// 根据指定字段排序后倒序查询单条数据
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultOrderByDescAsync<TKey>(Expression<Func<TEntity, TKey>> keySelector) => await this._currentRepository.FirstOrDefaultOrderByDescAsync(keySelector);
        /// <summary>
        /// 根据条件筛选后以指定字段排序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultOrderByAscAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector) => await this._currentRepository.FirstOrDefaultOrderByAscAsync(predicate, keySelector);
        /// <summary>
        /// 根据条件筛选后以指定字段倒序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        public virtual async Task<TEntity> FirstOrDefaultOrderByDescAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector) => await this._currentRepository.FirstOrDefaultOrderByDescAsync(predicate, keySelector);
        #endregion

        #region 查询集合数据
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetEntitiesAsync() => await this._currentRepository.GetEntitiesAsync();
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>>? predicate) => await this._currentRepository.GetEntitiesAsync(predicate);
        #endregion

        #region 查询分页数据
        /// <summary>
        /// 分页查询（根据ID倒序）
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <returns>返回查询到的数据</returns>
        public virtual async Task<PageResult<List<TEntity>>> GetPageEntitiesAsync(PageInParams pageInParams) => await this._currentRepository.GetPageEntitiesAsync(pageInParams);

        /// <summary>
        /// 分页查询（根据ID倒序）
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <param name="wherePredicate">查询条件</param>
        /// <returns>返回查询到的数据</returns>
        public virtual async Task<PageResult<List<TEntity>>> GetPageEntitiesAsync(PageInParams pageInParams, Expression<Func<TEntity, bool>>? wherePredicate) => await this._currentRepository.GetPageEntitiesAsync(pageInParams, wherePredicate);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <param name="wherePredicate">查询条件</param>
        /// <param name="orderByType">排序方式</param>
        /// <param name="orderByKeySelector">排序字段</param>
        /// <returns>返回查询到的数据</returns>
        public virtual async Task<PageResult<List<TEntity>>> GetPageEntitiesAsync<TKey>(PageInParams pageInParams, Expression<Func<TEntity, bool>>? wherePredicate, OrderByType orderByType, Expression<Func<TEntity, TKey>> orderByKeySelector) => await this._currentRepository.GetPageEntitiesAsync(pageInParams, wherePredicate, orderByType, orderByKeySelector);
        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">待添加的实体</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult<TEntity>> InsertAsync(TEntity entity) => await this._currentRepository.InsertAsync(entity);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entities">待添加的实体集合</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult<List<TEntity>>> InsertAsync(List<TEntity> entities) => await this._currentRepository.InsertAsync(entities);
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">待更新的实体</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult<TEntity>> UpdateAsync(TEntity entity) => await this._currentRepository.UpdateAsync(entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities">待更新的实体集合</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult<List<TEntity>>> UpdateAsync(List<TEntity> entities) => await this._currentRepository.UpdateAsync(entities);
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
            return await this._currentRepository.EnableAsync(entity, setEnableValue);
        }
        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <param name="entities">待启用/禁用的实体集合</param>
        /// <param name="setEnableValue">要设置的值</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult> EnableAsync<TEnableEntity>(List<TEnableEntity> entities, long setEnableValue) where TEnableEntity : IEnableSuperEntity, TEntity
        {
            return await this._currentRepository.EnableAsync(entities, setEnableValue);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">待删除的实体的ID</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult> DeleteAsync(long id, DeleteType deleteType = DeleteType.Logical)
        {
            TEntity? entity = await this._currentRepository.FirstOrDefaultAsync(n => n.Id == id);
            if (entity is null)
                return OperationResult.Success();

            return await this.DeleteAsync(entity, deleteType);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">待删除的实体</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult> DeleteAsync(TEntity entity, DeleteType deleteType = DeleteType.Logical) => await this._currentRepository.DeleteAsync(entity, deleteType);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">待删除的实体的ID集合</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult> DeleteAsync(List<long> ids, DeleteType deleteType = DeleteType.Logical)
        {
            List<TEntity> entities = this._currentRepository.GetEntities(n => ids.Contains(n.Id)).ToList();
            if (entities.Count == 0)
                return OperationResult.Success();

            return await this.DeleteAsync(entities, deleteType);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entities">待删除的实体集合</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        public virtual async Task<OperationResult> DeleteAsync(List<TEntity> entities, DeleteType deleteType = DeleteType.Logical) => await this._currentRepository.DeleteAsync(entities, deleteType);
        #endregion
        #endregion
    }
}
