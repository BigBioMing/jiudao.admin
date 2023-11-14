using JDA.Core.Exceptions;
using JDA.Core.Loggers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.MiddleWares
{
    public class LoggerScopeMiddleWare
    {
        private readonly RequestDelegate next;
        public LoggerScopeMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public virtual async Task InvokeAsync(HttpContext context, IHttpContextAccessor httpContextAccessor, ILogger<LoggerScopeMiddleWare> logger)
        {
            using (logger.BeginScope("ScopeId:{CurrentScopeId}", Guid.NewGuid()))
            //using (LogContext.PushProperty("CurrentScopeId", Guid.NewGuid()))
            {
                //string path = context.Request.Path.ToString();
                //if (path.Contains("/api"))
                //    throw new BusinessException("scope错误");
                await next(context);
            }
        }
    }
}
