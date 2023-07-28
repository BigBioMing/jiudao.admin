using JDA.Core.DI.Abstractions;
using JDA.Core.System.Abstractions;
using JDA.Core.System.Implements;
using JDA.Core.Users.Abstractions;
using JDA.Core.Users.Implements;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.System.DI
{
    /// <summary>
    /// 注册系统运行信息服务
    /// </summary>
    public class SystemRuntimeDependencyInjectionRegistrar : IDependencyInjectionRegistrar
    {
        public int Order => 1;

        public string Key => "SystemRuntime";

        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMachineInformation, DefaultMachineInformation>();
            serviceCollection.AddScoped<IApplicationRuntimeInformation, DefaultApplicationRuntimeInformation>();
            serviceCollection.AddScoped<ISystemPlatformInformation, DefaultSystemPlatformInformation>();
            serviceCollection.AddScoped<ISystemRuntimeEnvironmentInformation, DefaultSystemRuntimeEnvironmentInformation>();
        }
    }
}
