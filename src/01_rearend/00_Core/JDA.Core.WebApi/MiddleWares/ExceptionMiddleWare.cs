using JDA.Core.Exceptions;
using JDA.Core.Formats.WebApi;
using JDA.Core.Loggers;
using JDA.Core.WebApi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate next;
        public ExceptionMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public virtual async Task InvokeAsync(HttpContext context, IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                LoggerHelper.Default.Error(ex, "[2]");
                //判断是否是已知的异常
                if (ex is BusinessException businessException)
                {
                    var res = new UnifyResponse() { Message = businessException.Message, Code = businessException.Code };
                    context.Response.Clear();
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(res));
                }
                else
                {
                    var res = new UnifyResponse() { Message = ex.Message, Code = "50000" };
                    context.Response.StatusCode = 500;
                    context.Response.Clear();
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(res));
                }
            }
        }
    }
}
