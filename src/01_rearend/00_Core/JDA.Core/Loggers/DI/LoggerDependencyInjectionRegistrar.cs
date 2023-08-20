using JDA.Core.DI.Abstractions;
using JDA.Core.Loggers.Abstractions;
using JDA.Core.Loggers.Implements;
using JDA.Core.Mappers.Abstractions;
using JDA.Core.Mappers.Implements;
using JDA.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Loggers.DI
{
    public class LoggerDependencyInjectionRegistrar : IDependencyInjectionRegistrar
    {
        public int Order => 1;

        public string Key => "Logger";

        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ILogger, Logger>();
        }
    }
}
