using JDA.Core.Exceptions;
using JDA.Core.Formats.WebApi;
using JDA.Core.Loggers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.MiddleWares
{
    public class HappenExceptionMiddleWare
    {
        private RequestDelegate _next;
        public HappenExceptionMiddleWare(RequestDelegate next)
        {
            this._next = next;
        }

        public virtual async Task InvokeAsync(HttpContext context, IHttpContextAccessor httpContextAccessor)
        {
            await _next(context);
            string path = context.Request.Path.ToString();
            if (path.Contains("/api"))
                throw new NotImplementedException();
        }
    }
}
