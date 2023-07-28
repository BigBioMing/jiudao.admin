using JDA.Core.DI.Abstractions;
using JDA.Core.Persistence.Repositories.Abstractions;
using JDA.Core.Persistence.Repositories.Implements;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Repositories.DI
{
    /// <summary>
    /// 注册仓储服务
    /// </summary>
    public class RepositoryDependencyInjectionRegistrar : IDependencyInjectionRegistrar
    {
        public int Order => 1;

        public string Key => "Repository";

        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
