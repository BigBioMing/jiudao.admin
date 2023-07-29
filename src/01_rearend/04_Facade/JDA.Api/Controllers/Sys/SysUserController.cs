using JDA.Core.Formats.WebApi;
using JDA.Core.Models.FilterParamses;
using JDA.Core.Models.Operations;
using JDA.Core.Models.Tables;
using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Views.ViewModels;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using System.Linq.Expressions;

namespace JDA.Api.Controllers.Sys
{
    [Area("Sys")]
    [Route("api/[controller]")]
    [ApiController]
    public partial class SysUserController : ControllerBase
    {
        protected readonly ISysUserService _sysUserService;
        public SysUserController(ISysUserService sysUserService)
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
        public virtual async Task<IActionResult> GetPageEntities([FromQuery] FilterParams filterParams)
        {
            Expression<Func<SysUser, bool>>? predicate = null;
            string? name = filterParams?.Params?.Name;
            if (!string.IsNullOrWhiteSpace(name))
                predicate = n => n.Name.Contains(name);
            string? account = filterParams?.Params?.Account;
            if (!string.IsNullOrWhiteSpace(account))
                predicate = n => n.Account == account;

            var pageResult = await this._sysUserService.GetPageEntitiesAsync(filterParams.Page, predicate);

            return new JsonResult(pageResult);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model">待添加的数据模型</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public virtual async Task<UnifyResponse<object>> Save([FromBody] SysUser model)
        {
            OperationResult<SysUser> operationResult;
            //Id>0即为修改，Id<0即为添加
            if (model.Id > 0)
            {
                operationResult = await this._sysUserService.UpdateAsync(model);
            }
            else
            {
                operationResult = await this._sysUserService.InsertAsync(model);
            }

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
            List<SysUser> entities = this._sysUserService.GetEntities(n => model.Ids.Contains(n.Id)).ToList();
            if (entities.Count == 0) return UnifyResponse<object>.Error("数据不存在，无法启用/禁用");
            OperationResult operationResult = await this._sysUserService.EnableAsync(entities, model.SetEnableValue);

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
            OperationResult operationResult = await this._sysUserService.DeleteAsync(model.Id);

            if (operationResult.Status != JDA.Core.Models.Operations.OperationResultStatus.Success)
                return UnifyResponse<object>.Error(operationResult.Message);

            return UnifyResponse<object>.Success();
        }
    }
}
