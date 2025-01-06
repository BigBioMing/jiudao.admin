using JDA.Core.Formats.WebApi;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Users.Abstractions;
using JDA.Core.Views.ViewModels;
using JDA.Core.WebApi.ApiDocs;
using JDA.Core.WebApi.ControllerBases;
using JDA.DTO.SysActionResources;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using JDA.Model.Sys.SysActionResources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace JDA.Api.Controllers.Sys
{
    /// <summary>
    /// 路由资源
    /// </summary>
    [Area("Sys")]
    //[ApiVersion(Version = ApiVersionDefine.V2)]
    public class SysActionResourceController : BaseApiController<SysActionResource>
    {
        private readonly ISysActionResourceService _sysActionResourceService;
        public SysActionResourceController(ICurrentRunningContext currentRunningContext, IService<SysActionResource> currentService, ISysActionResourceService sysActionResourceService) : base(currentRunningContext, currentService)
        {
            _sysActionResourceService = sysActionResourceService;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="filterParams">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPageEntities")]
        public virtual async Task<IActionResult> GetPageEntities([FromQuery] SysActionResourceGetListVO filterParams)
        {
            Expression<Func<SysActionResource, bool>>? predicate = null;
            string? code = filterParams?.Code;
            if (!string.IsNullOrWhiteSpace(code))
                predicate = n => n.Code == code;
            string? name = filterParams?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            long? routeResourceId = filterParams?.RouteResourceId;
            if (routeResourceId != null)
                predicate = n => n.RouteResourceId == routeResourceId;

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
        public virtual async Task<UnifyResponse<SysActionResource>> Save([FromBody] SysActionResourceSaveInputDto model)
        {
            var operationResult = await _sysActionResourceService.SaveAsync(model);
            return operationResult.ToResponse();
        }

        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <param name="model">待启用/禁用的数据的标识</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Enable")]
        public virtual async Task<UnifyResponse<object>> Enable([FromBody] EnableListViewModel model)
        {
            return await base.EnableAsync<SysActionResource>(model);
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
            Expression<Func<SysActionResource, bool>>? predicate = null;
            string? name = filterParams?.Params?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            string? code = filterParams?.Params?.Code;
            if (!string.IsNullOrWhiteSpace(code))
                predicate = n => n.Code == code;

            var list = await this._currentService.GetEntitiesAsync(predicate);
            string fileName = $"操作资源_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.xlsx";
            return await base.ExportAsync(fileName, list);
        }
    }
}
