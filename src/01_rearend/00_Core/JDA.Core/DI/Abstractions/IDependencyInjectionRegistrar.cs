using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.DI.Abstractions
{
    /// <summary>
    /// 依赖注入注册器
    /// </summary>
    public interface IDependencyInjectionRegistrar
    {
        /// <summary>
        /// 注册顺序
        /// </summary>
        int Order { get; }
        /// <summary>
        /// Key
        /// </summary>
        string Key { get; }

        void Register(IServiceCollection serviceCollection);
    }
}
