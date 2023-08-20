using JDA.Core.Exceptions;
using JDA.Core.Formats.WebApi;
using JDA.Core.Loggers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.Filters
{
    public class ApiExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.ExceptionHandled == false && context.Exception is not null)
            {
                LoggerHelper.Default.Error(context.Exception, "[1]");
                //判断是否是已知的异常
                if (context.Exception is BusinessException businessException)
                {
                    var res = new UnifyResponse() { Message = businessException.Message, Code = businessException.Code };
                    await context.HttpContext.Response.WriteAsJsonAsync(res);
                }
                else
                {
                    var res = new UnifyResponse() { Message = context.Exception.Message, Code = "50000" };
                    context.HttpContext.Response.StatusCode = 500;
                    await context.HttpContext.Response.WriteAsJsonAsync(res);
                }
                context.ExceptionHandled = false;
            }
        }
    }
}
