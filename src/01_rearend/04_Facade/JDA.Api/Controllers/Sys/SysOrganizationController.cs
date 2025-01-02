using JDA.Core.Formats.WebApi;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Users.Abstractions;
using JDA.Core.Views.ViewModels;
using JDA.Core.WebApi.ControllerBases;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
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
    /// 组织机构
    /// </summary>
    [Area("Sys")]
    public partial class SysOrganizationController : BaseApiController<SysOrganization>
    {
        protected readonly ISysOrganizationService _sysOrganizationService;
        public SysOrganizationController(ICurrentRunningContext currentRunningContext, ISysOrganizationService sysOrganizationService) : base(currentRunningContext, sysOrganizationService)
        {
            this._sysOrganizationService = sysOrganizationService;
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
            Expression<Func<SysOrganization, bool>>? predicate = null;
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
        /// 获取树状数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTree")]
        public virtual async Task<IActionResult> GetTree([FromQuery] NoPageViewModel filterParams)
        {
            Expression<Func<SysOrganization, bool>>? predicate = null;
            string? name = filterParams?.Params?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            string? code = filterParams?.Params?.Code;
            if (!string.IsNullOrWhiteSpace(code))
                predicate = n => n.Code == code;

            var list = await this._sysOrganizationService.GetEntitiesAsync(predicate);
            var treeNodes = _sysOrganizationService.GetTrees(list);

            return new JsonResult(new { treeNodes });
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model">待添加的数据模型</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public virtual async Task<UnifyResponse<object>> Save([FromBody] SysOrganization model)
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
        public virtual async Task<UnifyResponse<object>> Enable([FromBody] EnableListViewModel model)
        {
            return await base.EnableAsync<SysOrganization>(model);
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
            Expression<Func<SysOrganization, bool>>? predicate = null;
            string? name = filterParams?.Params?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            string? code = filterParams?.Params?.Code;
            if (!string.IsNullOrWhiteSpace(code))
                predicate = n => n.Code == code;

            var list = await this._currentService.GetEntitiesAsync(predicate);
            string fileName = $"组织机构_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.xlsx";
            return await base.ExportAsync(fileName, list);
        }
    }
}
