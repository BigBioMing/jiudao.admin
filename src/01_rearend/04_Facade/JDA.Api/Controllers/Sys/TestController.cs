using JDA.Core.Exceptions;
using JDA.Core.Formats.WebApi;
using JDA.Entity.Entities.Sys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JDA.Api.Controllers.Sys
{
    [Area("Sys")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// return new JsonResult(new { hhhh = 11111 });
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hhh1")]
        [ApiExplorerSettings(GroupName = "V1")]
        public virtual IActionResult hhh1()
        {
            return new JsonResult(new { hhhh = 11111 });
        }
        /// <summary>
        /// return new JsonResult(new { hhhh = 2222 });
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hhh2")]
        [ApiExplorerSettings(GroupName = "V1")]
        public virtual async Task<IActionResult> hhh2()
        {
            await Task.Run(() => Task.Delay(1000));
            return new JsonResult(new { hhhh = 2222 });
        }
        /// <summary>
        /// return UnifyResponse[object].Success("hhhh=33333");
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hhh3")]
        [ApiExplorerSettings(GroupName = "V1")]
        public virtual UnifyResponse<object> hhh3()
        {
            return UnifyResponse<object>.Success("hhhh=33333");
        }
        /// <summary>
        /// return new JsonResult(UnifyResponse[object].Success("hhhh=44444"));
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hhh4")]
        [ApiExplorerSettings(GroupName = "V1")]
        public virtual async Task<IActionResult> hhh4()
        {
            await Task.Run(() => Task.Delay(1000));
            return new JsonResult(UnifyResponse<object>.Success("hhhh=44444"));
        }
        /// <summary>
        /// return UnifyResponse[dynamic].Success("hhhh=55555", new { h5 = "h5" });
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hhh5")]
        [ApiExplorerSettings(GroupName = "V1")]
        public virtual UnifyResponse<dynamic> hhh5()
        {
            return UnifyResponse<dynamic>.Success("hhhh=55555", new { h5 = "h5" });
        }
        /// <summary>
        /// return UnifyResponse[SysUser].Success("hhhh=66666", new SysUser() { Id = 1,Name = "张三" });
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hhh6")]
        [ApiExplorerSettings(GroupName = "V1")]
        public virtual UnifyResponse<SysUser> hhh6()
        {
            return UnifyResponse<SysUser>.Success("hhhh=66666", new SysUser() { Id = 1, Name = "张三" });
        }
        /// <summary>
        /// return new List[SysUser]() { new SysUser() { Name = "张三" }, new SysUser() { Name = "李四" } };
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hhh7")]
        [ApiExplorerSettings(GroupName = "V1")]
        public virtual IEnumerable<SysUser> hhh7()
        {
            return new List<SysUser>() { new SysUser() { Name = "张三" }, new SysUser() { Name = "李四" } };
        }
        /// <summary>
        /// return new SysUser() { Id = 12, Name = "王五" };
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hhh8")]
        [ApiExplorerSettings(GroupName = "V1")]
        public virtual SysUser hhh8()
        {
            return new SysUser() { Id = 12, Name = "王五" };
        }
        /// <summary>
        /// return "赵六";
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hhh9")]
        [ApiExplorerSettings(GroupName = "V1")]
        public virtual string hhh9()
        {
            return "赵六";
        }
        /// <summary>
        /// return "齐七";
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hhh10")]
        [ApiExplorerSettings(GroupName = "V1")]
        public virtual string hhh10()
        {
            throw new BusinessException("我是异常");
            return "齐七";
        }
        /// <summary>
        /// return UnifyResponse[SysUser].Success("hhhh=1111111111", new SysUser() { Id = 1, Name = "杨八" });
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hhh11")]
        [ApiExplorerSettings(GroupName = "V1")]
        public virtual UnifyResponse<SysUser> hhh11()
        {
            throw new BusinessException("我是异常");
            return UnifyResponse<SysUser>.Success("hhhh=1111111111", new SysUser() { Id = 1, Name = "杨八" });
        }
        /// <summary>
        /// return;
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hhh12")]
        [ApiExplorerSettings(GroupName = "V1")]
        public virtual void hhh12()
        {
            return;
        }
        /// <summary>
        /// await Task.CompletedTask;
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hhh13")]
        [ApiExplorerSettings(GroupName = "V1")]
        public virtual async Task hhh13()
        {
            await Task.CompletedTask;
        }
    }
}
