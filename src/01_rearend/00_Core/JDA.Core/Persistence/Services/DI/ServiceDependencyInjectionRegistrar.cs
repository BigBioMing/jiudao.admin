using JDA.Core.DI.Abstractions;
using JDA.Core.Persistence.Repositories.Abstractions;
using JDA.Core.Persistence.Repositories.Implements;
using JDA.Core.Persistence.Services.Abstractions;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Persistence.Services.Implements;
using JDA.Core.Persistence.Services.Implements.Default;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Services.DI
{
    /// <summary>
    /// 注册Service服务
    /// </summary>
    public class ServiceDependencyInjectionRegistrar : IDependencyInjectionRegistrar
    {
        public int Order => 1;

        public string Key => "Service";

        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IService<>), typeof(Service<>));
        }
    }
}
