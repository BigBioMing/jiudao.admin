using JDA.Core.Formats.WebApi;
using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Views.ViewModels;
using JDA.Core.WebApi.ControllerBases;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using JDA.Service.Sys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Api.Controllers.Sys
{
    /// <summary>
    /// 字典定义
    /// </summary>
    [Area("Sys")]
    public partial class SysDictionaryDefineController : BaseApiController<SysDictionaryDefine>
    {
        protected readonly ISysDictionaryDefineService _sysDictionaryDefineService;
        public SysDictionaryDefineController(ISysDictionaryDefineService sysDictionaryDefineService) : base(sysDictionaryDefineService)
        {
            this._sysDictionaryDefineService = sysDictionaryDefineService;
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
            Expression<Func<SysDictionaryDefine, bool>>? predicate = null;
            string? name = filterParams?.Params?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            string? code = filterParams?.Params?.Code;
            if (!string.IsNullOrWhiteSpace(code))
                predicate = n => n.Code == code;

            var pageResult = await base.GetPageEntitiesAsync(filterParams, predicate);

            return new JsonResult(pageResult);
        }

        /// <summary>
        /// 获取数据（包括字典项数据）
        /// </summary>
        /// <param name="filterParams">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDictionaryTree")]
        public virtual async Task<IActionResult> GetDictionaryTree([FromQuery] NoPageViewModel filterParams)
        {
            Expression<Func<SysDictionaryDefine, bool>>? predicate = null;
            string? name = filterParams?.Params?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            string? code = filterParams?.Params?.Code;
            if (!string.IsNullOrWhiteSpace(code))
                predicate = n => n.Code == code;

            var pageResult = await _sysDictionaryDefineService.GetDictionaryTree(predicate);

            return new JsonResult(pageResult);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model">待添加的数据模型</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public virtual async Task<UnifyResponse<object>> Save([FromBody] SysDictionaryDefine model)
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
            List<SysDictionaryDefine> entities = await this._currentService.GetEntitiesAsync(n => model.Ids.Contains(n.Id));
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
            Expression<Func<SysDictionaryDefine, bool>>? predicate = null;
            string? name = filterParams?.Params?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            string? code = filterParams?.Params?.Code;
            if (!string.IsNullOrWhiteSpace(code))
                predicate = n => n.Code == code;

            var list = await this._currentService.GetEntitiesAsync(predicate);
            string fileName = $"字典定义_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.xlsx";
            return await base.ExportAsync(fileName, list);
        }
    }
}
