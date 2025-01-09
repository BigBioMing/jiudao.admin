using JDA.Core.Formats.WebApi;
using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Users.Abstractions;
using JDA.Core.Views.ViewModels;
using JDA.Core.WebApi.ControllerBases;
using JDA.Entity.Entities.Sys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Api.Controllers.Sys
{
    /// <summary>
    /// 字典值数据
    /// </summary>
    [Area("Sys")]
    public partial class SysDictionaryDataController : BaseApiController<SysDictionaryData>
    {
        public SysDictionaryDataController(ICurrentRunningContext currentRunningContext, IService<SysDictionaryData> currentService) : base(currentRunningContext, currentService)
        {
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="filterParams">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPageEntities")]
        public virtual async Task<IActionResult> GetPageEntities([FromQuery] PageViewModel filterParams)
        {
            Expression<Func<SysDictionaryData, bool>>? predicate = null;
            string? name = filterParams?.Params?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            string? code = filterParams?.Params?.Code;
            if (!string.IsNullOrWhiteSpace(code))
                predicate = n => n.Code == code;
            long? dictionaryDefineId = filterParams?.Params?.DictionaryDefineId;
            if (dictionaryDefineId > 0)
                predicate = n => n.DictionaryDefineId == dictionaryDefineId;
            long? parentId = filterParams?.Params?.ParentId;
            if (parentId > 0)
                predicate = n => n.ParentId == parentId;

            var pageResult = await base.GetPageEntitiesAsync(filterParams, predicate);

            return new JsonResult(pageResult);
        }

        /// <summary>
        /// 根据id获取单条数据
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEntityById")]
        //[ApiExplorerSettings(GroupName = "V2")]
        public virtual async Task<IActionResult> GetEntityById([FromQuery] long id)
        {
            var entity = await base.GetEntityByIdAsync(id);

            return new JsonResult(entity);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model">待添加的数据模型</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public virtual async Task<UnifyResponse<object>> Save([FromBody] SysDictionaryData model)
        {
            return await base.SaveAsync(model);
        }

        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <param name="model">待启用/禁用的数据的标识</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Enable")]
        public virtual async Task<UnifyResponse<object>> Enable([FromBody] DicEnableListViewModel model)
        {
            List<SysDictionaryData> entities = await this._currentService.GetEntitiesAsync(n => model.Ids.Contains(n.Id));
            if (entities.Count == 0) return UnifyResponse<object>.Error("数据不存在，无法启用/禁用");
            entities.ForEach(item => item.Enabled = model.SetEnableValue);
            OperationResult operationResult = await this._currentService.UpdateAsync(entities);

            if (operationResult.Status != JDA.Core.Models.Operations.OperationResultStatus.Success)
                return UnifyResponse<object>.Error(operationResult.Message);

            return UnifyResponse<object>.Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="model">待删除的数据的标识</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Delete")]
        public virtual async Task<UnifyResponse<object>> Delete([FromBody] DeleteViewModel model)
        {
            return await base.DeleteAsync(model);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="filterParams">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Export")]
        public virtual async Task<IActionResult> Export([FromQuery] NoPageViewModel filterParams)
        {
            Expression<Func<SysDictionaryData, bool>>? predicate = null;
            string? name = filterParams?.Params?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            string? code = filterParams?.Params?.Code;
            if (!string.IsNullOrWhiteSpace(code))
                predicate = n => n.Code == code;
            long? dictionaryDefineId = filterParams?.Params?.DictionaryDefineId;
            if (dictionaryDefineId > 0)
                predicate = n => n.DictionaryDefineId == dictionaryDefineId;
            long? parentId = filterParams?.Params?.ParentId;
            if (parentId > 0)
                predicate = n => n.ParentId == parentId;

            var list = await this._currentService.GetEntitiesAsync(predicate);
            string fileName = $"字典选项_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.xlsx";
            return await base.ExportAsync(fileName, list);
        }
    }
}
