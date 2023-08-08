﻿using AutoMapper;
using JDA.Core.Attributes;
using JDA.Core.Formats.WebApi;
using JDA.Core.Models.Operations;
using JDA.Core.Models.Tables;
using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Utilities;
using JDA.Core.Views.ViewModels;
using JDA.Core.WebApi.ApiDocs;
using JDA.Core.WebApi.ControllerBases;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using JDA.Model.Sys.SysUsers;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace JDA.Api.Controllers.Sys
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Area("Sys")]
    [ApiVersion(Version = ApiVersionzDefine.V1)]
    public partial class SysUserController : BaseApiController<SysUser>
    {
        protected readonly ISysUserService _sysUserService;
        public SysUserController(ISysUserService sysUserService) : base(sysUserService)
        {
            this._sysUserService = sysUserService;
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
            Expression<Func<SysUser, bool>>? predicate = null;
            string? name = filterParams?.Params?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            string? account = filterParams?.Params?.Account;
            if (!string.IsNullOrWhiteSpace(account))
                predicate = n => n.Account == account;

            var pageResult = await base.GetPageEntitiesAsync(filterParams, predicate);

            return new JsonResult(pageResult);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model">待添加的数据模型</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public virtual async Task<UnifyResponse<object>> Save([FromBody] SysUserSaveVO model)
        {
            var operationResult = await _sysUserService.SaveAsync(model);
            if (operationResult.Status != JDA.Core.Models.Operations.OperationResultStatus.Success)
                return UnifyResponse<object>.Error(operationResult.Message);

            return UnifyResponse<object>.Success();
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
            return await base.EnableAsync<SysUser>(model);
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
            Expression<Func<SysUser, bool>>? predicate = null;
            string? name = filterParams?.Params?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            string? account = filterParams?.Params?.Account;
            if (!string.IsNullOrWhiteSpace(account))
                predicate = n => n.Account == account;

            var list = this._currentService.GetEntities(predicate).ToList();
            string fileName = $"用户_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.xlsx";
            return await base.ExportAsync(fileName, list);
        }
    }
}
