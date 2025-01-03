using JDA.Core.Formats.WebApi;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Users.Abstractions;
using JDA.Core.Views.ViewModels;
using JDA.Core.WebApi.ControllerBases;
using JDA.DTO.SysRoles;
using JDA.DTO.SysUsers;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using JDA.Model.Sys.SysRoles;
using JDA.Model.Sys.SysUsers;
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
    /// 角色
    /// </summary>
    [Area("Sys")]
    public partial class SysRoleController : BaseApiController<SysRole>
    {
        protected readonly ISysRoleService _sysRoleService;
        public SysRoleController(ICurrentRunningContext currentRunningContext, ISysRoleService sysRoleService) : base(currentRunningContext, sysRoleService)
        {
            this._sysRoleService = sysRoleService;
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
            Expression<Func<SysRole, bool>>? predicate = null;
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
        /// 保存数据
        /// </summary>
        /// <param name="model">待添加的数据模型</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public virtual async Task<UnifyResponse<object>> Save([FromBody] SysRoleSaveVO model)
        {
            //return await base.SaveAsync(model)
            return null;
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
            return await base.EnableAsync<SysRole>(model);
        }

        /// <summary>
        /// 给角色授权
        /// </summary>
        /// <param name="model">授权信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Empower")]
        public virtual async Task<UnifyResponse<object>> Empower([FromBody] SysRoleEmpowerVO model)
        {
            var operationResult = await _sysRoleService.Empower(model.RoleId, model.RouteResourceIds, model.ActionResourceIds);
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
            Expression<Func<SysRole, bool>>? predicate = null;
            string? name = filterParams?.Params?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            string? code = filterParams?.Params?.Code;
            if (!string.IsNullOrWhiteSpace(code))
                predicate = n => n.Code == code;

            var list = await this._currentService.GetEntitiesAsync(predicate);
            string fileName = $"角色_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.xlsx";
            return await base.ExportAsync(fileName, list);
        }

        /// <summary>
        /// 获取按钮和菜单权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Policy = "Permission")]
        [Route("GetRouteAndOptions")]
        public virtual async Task<UnifyResponse<MenuAndActionDto>> GetRouteAndOptions([FromQuery]long roleId)
        {
            var model = await _sysRoleService.GetMenuAndActions(roleId);
            return UnifyResponse<MenuAndActionDto>.Success(model);
        }
    }
}
