using JDA.Core.DI.Abstractions;
using JDA.Core.Users.Abstractions;
using JDA.Core.Users.Implements;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Users.DI
{
    /// <summary>
    /// 注册当前执行人
    /// </summary>
    public class UsersDependencyInjectionRegistrar : IDependencyInjectionRegistrar
    {
        public int Order => 1;

        public string Key => "Users";

        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICurrentRunningContext, DefaultCurrentRunningContext>();
        }
    }
}
