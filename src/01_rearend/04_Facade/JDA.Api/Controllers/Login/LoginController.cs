using JDA.Core.Formats.WebApi;
using JDA.Core.Models.Operations;
using JDA.Core.Tokens;
using JDA.Core.Users.Abstractions;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using JDA.Model.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace JDA.Api.Controllers.Login
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ISysUserService _sysUserService;
        private readonly ICurrentRunningContext _currentRunningContext;
        public LoginController(ISysUserService sysUserService, ICurrentRunningContext currentRunningContext)
        {
            this._sysUserService = sysUserService;
            this._currentRunningContext = currentRunningContext;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model">用户登录输入参数</param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<IActionResult> Login([FromBody] LoginInputVO model)
        {
            //校验用户是否存在
            var user = await this._sysUserService.FirstOrDefaultAsync(n => n.Account == model.UserName);
            if (user == null) return new JsonResult(UnifyResponse<object>.Error("用户名或密码错误"));

            //校验密码是否正确
            if (user.Password != model.Password) return new JsonResult(UnifyResponse<object>.Error("用户名或密码错误"));

            //查询用户角色
            var roles = await this._sysUserService.GetRolesAsync(user);

            //生成token
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Account)
            };
            if (roles.Data != null)
            {
                foreach (var role in roles.Data)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Code));
                }
            }
            var token = TokenHelper.GetToken(claims);

            return new JsonResult(UnifyResponse<object>.Success("登录成功", token));
        }
    }
}
