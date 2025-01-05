using JDA.Core.Attributes;
using JDA.Core.Formats.WebApi;
using JDA.Core.Models.Operations;
using JDA.Core.Models.Tables;
using JDA.Core.Persistence.Entities;
using JDA.Core.Persistence.Entities.Abstractions;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Users.Abstractions;
using JDA.Core.Utilities;
using JDA.Core.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.ControllerBases
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    public abstract class BaseApiController<TEntity> : ControllerBase where TEntity : class, ISuperEntity
    {
        protected readonly IService<TEntity> _currentService;
        protected readonly ICurrentRunningContext _currentRunningContext;
        public BaseApiController(ICurrentRunningContext currentRunningContext, IService<TEntity> currentService)
        {
            this._currentService = currentService;
            this._currentRunningContext = currentRunningContext;
        }

        #region 分页
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="filterParams">查询条件</param>
        /// <returns></returns>
        protected virtual async Task<PageResult<List<TEntity>>> GetPageEntitiesAsync(PageViewModel filterParams, Expression<Func<TEntity, bool>>? predicate)
        {
            var pageResult = await this._currentService.GetPageEntitiesAsync(new PageInParams() { PageIndex = filterParams?.PageIndex ?? 1, PageSize = filterParams?.PageSize ?? 10 }, predicate);
            return pageResult;
        }
        #endregion

        #region 获取全部数据
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <param name="filterParams">查询条件</param>
        /// <returns></returns>
        protected virtual async Task<List<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>>? predicate)
        {
            var result = await this._currentService.GetEntitiesAsync(predicate);
            return result;
        }
        #endregion

        #region 获取单条数据
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="filterParams">查询条件</param>
        /// <returns></returns>
        protected virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            TEntity? entity = await this._currentService.FirstOrDefaultAsync(predicate);
            return entity;
        }
        /// <summary>
        /// 根据Id获取单条数据
        /// </summary>
        /// <param name="filterParams">查询条件</param>
        /// <returns></returns>
        protected virtual async Task<TEntity?> GetEntityByIdAsync(long id)
        {
            TEntity? entity = await this._currentService.FirstOrDefaultAsync(n => n.Id == id);
            return entity;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model">待添加的数据模型</param>
        /// <returns></returns>
        protected virtual async Task<UnifyResponse<object>> SaveAsync(TEntity model)
        {
            OperationResult<TEntity> operationResult;
            //Id>0即为修改，Id<0即为添加
            if (model.Id > 0)
            {
                operationResult = await this._currentService.UpdateAsync(model);
            }
            else
            {
                operationResult = await this._currentService.InsertAsync(model);
            }

            if (operationResult.Status != JDA.Core.Models.Operations.OperationResultStatus.Success)
                return UnifyResponse<object>.Error(operationResult.Message);

            return UnifyResponse<object>.Success();
        }
        #endregion

        #region 启用/禁用
        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <param name="model">待启用/禁用的数据的标识</param>
        /// <returns></returns>
        protected virtual async Task<UnifyResponse<object>> EnableAsync<TEnableEntity>(EnableListViewModel model) where TEnableEntity : IEnableSuperEntity, TEntity
        {
            List<TEntity> entities = await this._currentService.GetEntitiesAsync(n => model.Ids.Contains(n.Id));
            List<TEnableEntity> enableEntities = entities as List<TEnableEntity>;
            if (entities.Count == 0) return UnifyResponse<object>.Error("数据不存在，无法启用/禁用");
            OperationResult operationResult = await this._currentService.EnableAsync(enableEntities, model.SetEnableValue);

            if (operationResult.Status != JDA.Core.Models.Operations.OperationResultStatus.Success)
                return UnifyResponse<object>.Error(operationResult.Message);

            return UnifyResponse<object>.Success();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="model">待删除的数据的标识</param>
        /// <returns></returns>
        protected virtual async Task<UnifyResponse<object>> DeleteAsync(DeleteViewModel model)
        {
            OperationResult operationResult = await this._currentService.DeleteAsync(model.Id);

            if (operationResult.Status != JDA.Core.Models.Operations.OperationResultStatus.Success)
                return UnifyResponse<object>.Error(operationResult.Message);

            return UnifyResponse<object>.Success();
        }
        #endregion

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="fileName">文件名称</param>
        /// <param name="list">要导出的数据集合</param>
        /// <returns></returns>
        protected virtual async Task<IActionResult> ExportAsync<T>(string fileName, List<T> list)
        {
            string filePath = $"{AppContext.BaseDirectory}/wwwroot/Upload/{fileName}.xlsx";
            Dictionary<string, ColumnMetadataAttribute> columnMetadatas = AttributeHelper.GetAttributeInfo<ColumnMetadataAttribute>(typeof(T));
            return await ExportAsync<T>(list, filePath);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="list">要导出的数据集合</param>
        /// <param name="filePath">要保存的文件路径</param>
        /// <returns></returns>
        protected virtual async Task<IActionResult> ExportAsync<T>(List<T> list, string filePath)
        {
            Dictionary<string, ColumnMetadataAttribute> columnMetadatas = AttributeHelper.GetAttributeInfo<ColumnMetadataAttribute>(typeof(T));
            DataTable dt = DataTableHelper.ToDataTable(list);
            string fileName = Path.GetFileName(filePath);
            await ExcelHelper.ExportAsync(dt, columnMetadatas, filePath);
            var file = FileHelper.ReadAllBytes(filePath);
            FileHelper.Delete(filePath);
            return File(file, "application/ms-excel", fileName);
        }
        #endregion
    }
}
