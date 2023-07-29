using JDA.Core.Models.Deletes;
using JDA.Core.Models.Operations;
using JDA.Core.Models.OrderBys;
using JDA.Core.Models.Tables;
using JDA.Core.Persistence.Contexts;
using JDA.Core.Persistence.Entities;
using System.Linq.Expressions;

namespace JDA.Core.Persistence.Repositories.Abstractions
{
    /// <summary>
    /// 仓储基接口
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public interface IBaseRepository<TEntity, TDbContext> where TEntity : SuperEntity where TDbContext : JDABaseDbContext
    {
        public TDbContext DbContext { get; }

        #region Queryable
        /// <summary>
        /// 查询主体（过滤掉了被删除的数据）
        /// </summary>
        IQueryable<TEntity> Queryable { get; }
        /// <summary>
        /// 查询主体（包含被删除的数据）
        /// </summary>
        IQueryable<TEntity> QueryableAll { get; }
        /// <summary>
        /// 查询主体（过滤掉了被删除的数据）
        /// </summary>
        IQueryable<TEntity> QueryableNoTracking { get; }
        /// <summary>
        /// 查询主体（包含被删除的数据）
        /// </summary>
        IQueryable<TEntity> QueryableAllNoTracking { get; }
        #endregion

        #region 同步方法
        #region 查询单条数据
        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        TEntity? FirstOrDefault();
        /// <summary>
        /// 根据条件查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据ID排序后正序查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        TEntity? FirstOrDefaultOrderByAscById();
        /// <summary>
        /// 根据ID排序后倒序查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        TEntity? FirstOrDefaultOrderByDescById();
        /// <summary>
        /// 根据条件筛选后以ID排序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        TEntity? FirstOrDefaultOrderByAscById(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据条件筛选后以ID排序倒序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        TEntity? FirstOrDefaultOrderByDescById(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据指定字段排序后正序查询单条数据
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>

        TEntity? FirstOrDefaultOrderByAsc<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        /// <summary>
        /// 根据指定字段排序后倒序查询单条数据
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        TEntity? FirstOrDefaultOrderByDesc<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        /// <summary>
        /// 根据条件筛选后以指定字段排序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        TEntity? FirstOrDefaultOrderByAsc<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector);
        /// <summary>
        /// 根据条件筛选后以指定字段倒序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        TEntity? FirstOrDefaultOrderByDesc<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector);
        #endregion

        #region 查询集合数据
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetEntities();
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region 查询分页数据
        /// <summary>
        /// 分页查询（根据ID倒序）
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <param name="wherePredicate">查询条件</param>
        /// <returns>返回查询到的数据</returns>
        PageResult<List<TEntity>> GetPageEntities(PageInParams pageInParams, Expression<Func<TEntity, bool>> wherePredicate);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <param name="wherePredicate">查询条件</param>
        /// <param name="orderByType">排序方式</param>
        /// <param name="orderByKeySelector">排序字段</param>
        /// <returns>返回查询到的数据</returns>
        PageResult<List<TEntity>> GetPageEntities<TKey>(PageInParams pageInParams, Expression<Func<TEntity, bool>> wherePredicate, OrderByType orderByType, Expression<Func<TEntity, TKey>> orderByKeySelector);
        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">待添加的实体</param>
        /// <returns>返回操作结果</returns>
        OperationResult<TEntity> Insert(TEntity entity);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entities">待添加的实体集合</param>
        /// <returns>返回操作结果</returns>
        OperationResult<List<TEntity>> Insert(List<TEntity> entities);
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">待更新的实体</param>
        /// <returns>返回操作结果</returns>
        OperationResult<TEntity> Update(TEntity entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities">待更新的实体集合</param>
        /// <returns>返回操作结果</returns>
        OperationResult<List<TEntity>> Update(List<TEntity> entities);
        #endregion

        #region 删除
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">待删除的实体</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        OperationResult Delete(TEntity entity, DeleteType deleteType = DeleteType.Logical);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entities">待删除的实体集合</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        OperationResult Delete(List<TEntity> entities, DeleteType deleteType = DeleteType.Logical);
        #endregion
        #endregion

        #region 异步方法
        #region 查询单条数据
        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        Task<TEntity> FirstOrDefaultAsync();
        /// <summary>
        /// 根据条件查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据ID排序后正序查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        Task<TEntity> FirstOrDefaultOrderByAscByIdAsync();
        /// <summary>
        /// 根据ID排序后倒序查询单条数据
        /// </summary>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        Task<TEntity> FirstOrDefaultOrderByDescByIdAsync();
        /// <summary>
        /// 根据条件筛选后以ID排序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        Task<TEntity> FirstOrDefaultOrderByAscByIdAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据条件筛选后以ID排序倒序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        Task<TEntity> FirstOrDefaultOrderByDescByIdAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据指定字段排序后正序查询单条数据
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>

        Task<TEntity> FirstOrDefaultOrderByAscAsync<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        /// <summary>
        /// 根据指定字段排序后倒序查询单条数据
        /// </summary>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        Task<TEntity> FirstOrDefaultOrderByDescAsync<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        /// <summary>
        /// 根据条件筛选后以指定字段排序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        Task<TEntity> FirstOrDefaultOrderByAscAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector);
        /// <summary>
        /// 根据条件筛选后以指定字段倒序正序查询单条数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序字段</param>
        /// <returns>返回序列的第一个元素，如果序列不包含元素，则返回默认值</returns>
        Task<TEntity> FirstOrDefaultOrderByDescAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector);
        #endregion

        #region 查询分页数据
        /// <summary>
        /// 分页查询（根据ID倒序）
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <param name="wherePredicate">查询条件</param>
        /// <returns>返回查询到的数据</returns>
        Task<PageResult<List<TEntity>>> GetPageEntitiesAsync(PageInParams pageInParams, Expression<Func<TEntity, bool>> wherePredicate);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageInParams">分页参数</param>
        /// <param name="wherePredicate">查询条件</param>
        /// <param name="orderByType">排序方式</param>
        /// <param name="orderByKeySelector">排序字段</param>
        /// <returns>返回查询到的数据</returns>
        Task<PageResult<List<TEntity>>> GetPageEntitiesAsync<TKey>(PageInParams pageInParams, Expression<Func<TEntity, bool>> wherePredicate, OrderByType orderByType, Expression<Func<TEntity, TKey>> orderByKeySelector);
        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">待添加的实体</param>
        /// <returns>返回操作结果</returns>
        Task<OperationResult<TEntity>> InsertAsync(TEntity entity);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entities">待添加的实体集合</param>
        /// <returns>返回操作结果</returns>
        Task<OperationResult<List<TEntity>>> InsertAsync(List<TEntity> entities);
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">待更新的实体</param>
        /// <returns>返回操作结果</returns>
        Task<OperationResult<TEntity>> UpdateAsync(TEntity entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entities">待更新的实体集合</param>
        /// <returns>返回操作结果</returns>
        Task<OperationResult<List<TEntity>>> UpdateAsync(List<TEntity> entities);
        #endregion

        #region 删除
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">待删除的实体</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        Task<OperationResult> DeleteAsync(TEntity entity, DeleteType deleteType = DeleteType.Logical);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entities">待删除的实体集合</param>
        /// <param name="deleteType">删除方式 逻辑删除/物理删除</param>
        /// <returns>返回操作结果</returns>
        Task<OperationResult> DeleteAsync(List<TEntity> entities, DeleteType deleteType = DeleteType.Logical);
        #endregion
        #endregion
    }
}
