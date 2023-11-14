﻿using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.MiddleWares
{
    public static class MiddleWareExtension
    {
        public static WebApplication UseLoggerScope(this WebApplication app)
        {
            app.UseMiddleware<LoggerScopeMiddleWare>();
            return app;
        }
        public static WebApplication UseException(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleWare>();
            return app;
        }
    }
}