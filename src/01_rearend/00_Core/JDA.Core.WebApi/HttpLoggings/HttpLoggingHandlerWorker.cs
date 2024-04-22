using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.HttpLoggings
{
    /// <summary>
    /// 请求日志入库worker
    /// </summary>
    public class HttpLoggingHandlerWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public HttpLoggingHandlerWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await HttpLoggingHandler.Handler(_serviceProvider);
        }
    }
}
