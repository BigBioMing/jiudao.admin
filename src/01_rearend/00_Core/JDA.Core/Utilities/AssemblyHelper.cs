using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Utilities
{
    public class AssemblyHelper
    {
        /// <summary>
        /// 获取所有程序集
        /// </summary>
        /// <returns></returns>
        public static List<Assembly> GetAssemblies()
        {
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
            assemblies = assemblies.Where(n => n.FullName.StartsWith("JDA.")).ToList();

            return assemblies;
        }
    }
}
