using JDA.Core.Persistence.Maps;
using JDA.Core.Utilities;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Contexts
{
    /// <summary>
    /// 数据库上下文基类
    /// </summary>
    public abstract class JDABaseDbContext : DbContext
    {
        public JDABaseDbContext()
        {

        }

        public JDABaseDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public int TenantId { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 通过反射获取所有继承了SuperMapping<>的类，并将其注册到EF中
            //mapping超类
            Type superMapping = typeof(SuperMapping<>);
            //var assembly = Assembly.GetEntryAssembly();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            assemblies = assemblies.Where(n => n.FullName.StartsWith("JDA.")).ToArray();

            var mappingTypes = assemblies.SelectMany(assembly => assembly.GetTypes().Where(type => TypeHelper.HasImplementedRawGeneric(type, superMapping) && !type.IsAbstract && !type.IsInterface)).ToList();

            foreach (var mappingType in mappingTypes)
            {
                #region MyRegion
                //object? mapping = Activator.CreateInstance(mappingType);
                //var sm = TypeHelper.FindBaseType(mappingType, superMapping);
                //var genericArguments = sm.GetGenericArguments();
                ////var entityTypeBuilderType = typeof(EntityTypeBuilder<>).MakeGenericType(genericArguments);
                //MethodInfo methodInfo = superMapping.GetMethod("Configure");
                //var modelBuilderType = modelBuilder.GetType();
                //var modelBuilderMethods = modelBuilderType.GetMethods();
                //var modelBuilderEntityMethodInfos = modelBuilderMethods.Where(n => n.Name == "Entity").Where(n => n.IsGenericMethod).ToList();
                //var modelBuilderEntityMethodInfo = modelBuilderEntityMethodInfos.FirstOrDefault(n => n.GetParameters().Length == 0);
                //modelBuilderEntityMethodInfo = modelBuilderEntityMethodInfo.MakeGenericMethod(genericArguments);
                //var p = modelBuilderEntityMethodInfo.Invoke(modelBuilder, null);
                //methodInfo.Invoke(mapping, null);
                #endregion

                // 创建mapping对象
                object? mappingInstance = Activator.CreateInstance(mappingType);
                // 获取 mapping 类上的泛型参数
                var sm = TypeHelper.FindBaseType(mappingType, superMapping);
                var genericArguments = sm.GetGenericArguments();
                var modelBuilderType = modelBuilder.GetType();
                // 反射获取 modelBuilder.ApplyConfiguration 方法
                MethodInfo modelBuilderApplyConfigurationMethodInfo = modelBuilderType.GetMethod("ApplyConfiguration");
                // 由于 modelBuilder.ApplyConfiguration 是泛型方法，需要执行 MakeGenericMethod 方法
                modelBuilderApplyConfigurationMethodInfo = modelBuilderApplyConfigurationMethodInfo.MakeGenericMethod(genericArguments);
                //执行 modelBuilder.ApplyConfiguration 方法，将mapping注册到EF中
                modelBuilderApplyConfigurationMethodInfo.Invoke(modelBuilder, new object[] { mappingInstance });
            }
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
