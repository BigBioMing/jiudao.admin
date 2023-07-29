using JDA.Core.DI.Abstractions;
using JDA.Core.Persistence.Maps;
using JDA.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.DI
{
    /// <summary>
    /// 依赖注入加载器
    /// </summary>
    public static class DependencyInjectionLoader
    {
        public static IServiceCollection AddDependencyInjectionService(this IServiceCollection services)
        {

            #region 通过反射获取所有实现了 IDependencyInjectionRegistrar 接口的类，并注入DI服务
            //超类
            Type superClass = typeof(IDependencyInjectionRegistrar);
            List<Assembly> assemblies = new List<Assembly>();
            DependencyContext.Default.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package")//排除所有的系统程序集、Nuget下载包
               .ToList().ForEach(item =>
               {
                   //if (item.Type == "project")//(item.Name.Contains("MES"))
                   //{
                   //    Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(item.Name));
                   //    assemblies.Add(assembly);
                   //}
                   Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(item.Name));
                   assemblies.Add(assembly);
               });

            //assemblies = DependencyContext.Default.CompileLibraries.Select(item=> AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(item.Name))).OrderBy(n => n.FullName).ToList();
            assemblies = assemblies.OrderBy(n => n.FullName).ToList();
            assemblies = assemblies.Where(n => n.FullName.StartsWith("JDA.")).ToList();
            var registerTypes = assemblies.SelectMany(assembly => assembly.GetTypes().Where(type => superClass.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)).ToList();

            DependencyContext dependencyContext = DependencyContext.Default;
            List<IDependencyInjectionRegistrar> registerInstances = new List<IDependencyInjectionRegistrar>();
            foreach (var registerType in registerTypes)
            {
                // 创建 register 对象
                object? registerInstance = Activator.CreateInstance(registerType);
                registerInstances.Add(registerInstance as IDependencyInjectionRegistrar);
            }

            registerInstances = registerInstances.OrderBy(n => n.Order).ToList();
            MethodInfo registerMethodInfo = superClass.GetMethod("Register");
            foreach (var registerInstance in registerInstances)
            {
                //执行 modelBuilder.ApplyConfiguration 方法，将 registerInstance 注册到EF中
                registerMethodInfo.Invoke(registerInstance, new object[] { services });
            }
            #endregion

            return services;
        }
    }
}
