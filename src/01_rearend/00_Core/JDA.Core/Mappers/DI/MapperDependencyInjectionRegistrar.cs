using JDA.Core.DI.Abstractions;
using JDA.Core.Mappers.Abstractions;
using JDA.Core.Mappers.Implements;
using JDA.Core.Persistence.Maps;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Core.Persistence.Services.Implements.Default;
using JDA.Core.Users.Abstractions;
using JDA.Core.Users.Implements;
using JDA.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Mappers.DI
{
    /// <summary>
    /// 注册对象映射
    /// </summary>
    public class MapperDependencyInjectionRegistrar : IDependencyInjectionRegistrar
    {
        public int Order => 1;

        public string Key => "Mapper";

        public void Register(IServiceCollection serviceCollection)
        {
            #region 通过反射获取所有实现了 IDependencyInjectionRegistrar 接口的类，并注入DI服务
            //超类
            Type superClass = typeof(ShapeWishProfile);
            List<Assembly> assemblies = AssemblyHelper.GetAssemblies();
            var registerTypes = assemblies.SelectMany(assembly => assembly.GetTypes().Where(type => TypeHelper.HasImplementedRawGeneric(type, superClass) && !type.IsAbstract && !type.IsInterface)).ToList();

            serviceCollection.AddAutoMapper(registerTypes.ToArray());
            serviceCollection.AddTransient<IShapeMapper, ShapeMapper>();
            #endregion
        }
    }
}
