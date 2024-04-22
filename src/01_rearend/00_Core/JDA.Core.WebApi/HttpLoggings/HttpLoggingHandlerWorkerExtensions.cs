using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.HttpLoggings
{
    public static class HttpLoggingHandlerWorkerExtensions
    {
        public static IServiceCollection AddHttpLoggingHandlerWorker(this IServiceCollection services)
        {
            services.AddHostedService<HttpLoggingHandlerWorker>();
            return services;
        }
    }
}
