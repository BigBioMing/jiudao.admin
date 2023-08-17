using JDA.Core.DI.Abstractions;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Persistence.Services.Implements.Default;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using JDA.Service.Sys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Service.DI
{
    /// <summary>
    /// 注册Service服务
    /// </summary>
    public class SpecificServiceDependencyInjectionRegistrar : IDependencyInjectionRegistrar
    {
        public int Order => 2;

        public string Key => "SpecificService";

        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IService<SysUser>, SysUserService>();
            serviceCollection.AddScoped<ISysUserService, SysUserService>();
            serviceCollection.AddScoped<ISysOrganizationService, SysOrganizationService>();
            serviceCollection.AddScoped<ISysDictionaryDefineService, SysDictionaryDefineService>();
        }
    }
}
