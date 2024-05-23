using JDA.Core.Exceptions;
using JDA.Core.Loggers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
            var jwt = await httpContextAccessor.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
            if (jwt.Succeeded)
            {
                var token = jwt.Ticket;
                httpContextAccessor.HttpContext.User = jwt.Principal;
                string? identifier = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                string? identifier2 = jwt.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                //// 解析JWT
                //var handler = new JwtSecurityTokenHandler();
                //var jwtToken = handler.ReadJwtToken(token);

                //// 使用jwtToken中的Claims
                //var someClaimValue = jwtToken.Claims.FirstOrDefault(c => c.Type == "some_claim_type")?.Value;

                // 做其他处理...
            }
            using (logger.BeginScope("ScopeId:{CurrentScopeId}", Guid.NewGuid().ToString("N")))
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
