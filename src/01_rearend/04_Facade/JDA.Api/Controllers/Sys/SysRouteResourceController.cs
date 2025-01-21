using JDA.Core.Formats.WebApi;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Users.Abstractions;
using JDA.Core.Views.ViewModels;
using JDA.Core.WebApi.ApiDocs;
using JDA.Core.WebApi.ControllerBases;
using JDA.DTO.SysActionResources;
using JDA.DTO.SysRoles;
using JDA.DTO.SysRouteResources;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using JDA.Model.Sys.SysRouteResources;
using JDA.Service.Sys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JDA.Api.Controllers.Sys
{
    /// <summary>
    /// 路由资源
    /// </summary>
    [Area("Sys")]
    [ApiVersion(Version = ApiVersionDefine.V2)]
    public partial class SysRouteResourceController : BaseApiController<SysRouteResource>
    {
        private readonly ISysRouteResourceService _sysRouteResourceService;
        public SysRouteResourceController(ICurrentRunningContext currentRunningContext, IService<SysRouteResource> currentService, ISysRouteResourceService sysRouteResourceService) : base(currentRunningContext, currentService)
        {
            _sysRouteResourceService = sysRouteResourceService;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="filterParams">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPageEntities")]
        public virtual async Task<IActionResult> GetPageEntities([FromQuery] SysRouteResourceGetListVO filterParams)
        {
            Expression<Func<SysRouteResource, bool>>? predicate = null;
            string? name = filterParams?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            string? code = filterParams?.Code;
            if (!string.IsNullOrWhiteSpace(code))
                predicate = n => n.Code == code;
            string? title = filterParams?.Title;
            if (!string.IsNullOrWhiteSpace(title))
                predicate = n => n.Title == title;

            var pageResult = await base.GetPageEntitiesAsync(filterParams, predicate);

            return new JsonResult(pageResult);
        }

        /// <summary>
        /// 获取全部路由资源
        /// </summary>
        /// <param name="filterParams">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEntities")]
        public virtual async Task<IActionResult> GetEntities([FromQuery] PageViewModel filterParams)
        {
            var result = await base.GetEntitiesAsync(null);

            return new JsonResult(result);
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
        /// 获取路由资源树
        /// </summary>
        /// <param name="filterParams">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRouteTree")]
        public virtual async Task<IActionResult> GetRouteTree([FromQuery] PageViewModel filterParams)
        {
            var result = await _sysRouteResourceService.GetRouteTreeAsync();

            return new JsonResult(result);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model">待添加的数据模型</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public virtual async Task<UnifyResponse<SysRouteResource>> Save([FromBody] SysRouteResourceSaveInputDto model)
        {
            var operationResult = await _sysRouteResourceService.SaveAsync(model);
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
            return await base.EnableAsync<SysRouteResource>(model);
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
        public virtual async Task<IActionResult> Export([FromQuery] SysRouteResourceImportDataVO filterParams)
        {
            Expression<Func<SysRouteResource, bool>>? predicate = null;
            string? name = filterParams?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            string? code = filterParams?.Code;
            if (!string.IsNullOrWhiteSpace(code))
                predicate = n => n.Code == code;
            string? title = filterParams?.Title;
            if (!string.IsNullOrWhiteSpace(title))
                predicate = n => n.Title == title;

            var list = await this._currentService.GetEntitiesAsync(predicate);
            string fileName = $"路由资源_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.xlsx";
            return await base.ExportAsync(fileName, list);
        }

        /// <summary>
        /// 获取按钮和菜单权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Policy = "Permission")]
        [Route("GetRouteAndActions")]
        public virtual async Task<UnifyResponse<List<MenuTreeDto>>> GetRouteAndActions()
        {
            var model = await _sysRouteResourceService.GetMenuAndActionsAsync();
            return UnifyResponse<List<MenuTreeDto>>.Success(model);
        }

        /// <summary>
        /// 获取指定菜单拥有的按钮集合
        /// </summary>
        /// <param name="routeResourceId">路由（菜单）Id</param>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Policy = "Permission")]
        [Route("GetActions")]
        public virtual async Task<UnifyResponse<List<SysActionResourceDto>>> GetActions([FromQuery] long routeResourceId)
        {
            var list = await _sysRouteResourceService.GetActionsAsync(routeResourceId);
            return UnifyResponse<List<SysActionResourceDto>>.Success(list);
        }

        /// <summary>
        /// 保存指定菜单下的按钮集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Policy = "Permission")]
        [Route("SaveActions")]
        public virtual async Task<UnifyResponse> SaveActions([FromBody] SaveRouteActionsVO model)
        {
            await _sysRouteResourceService.SaveActionsAsync(model.RouteResourceId, model.Actions);
            return UnifyResponse.Success();
        }
    }
}
