using JHLA.Core.Persistence.Entities;
using JHLA.Core.Persistence.Maps;
using JHLA.Core.Utilities;
using JHLA.Entity.Entities.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace JHLA.Entity.Contexts
{
    public partial class JHLADbContext : DbContext
    {
        public JHLADbContext(DbContextOptions<JHLADbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<SysUser> SysUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //mapping超类
            Type superMapping = typeof(SuperMapping<>);
            var assembly = Assembly.GetExecutingAssembly();
            var mappingTypes = assembly.GetTypes().Where(type => TypeHelper.HasImplementedRawGeneric(type, superMapping) && !type.IsAbstract && !type.IsInterface).ToList();

            foreach (var mappingType in mappingTypes)
            {
                object? mapping = Activator.CreateInstance(mappingType);
                var sm = TypeHelper.FindBaseType(mappingType, superMapping);
                var genericArguments = sm.GetGenericArguments();
                ////var entityTypeBuilderType = typeof(EntityTypeBuilder<>).MakeGenericType(genericArguments);
                //MethodInfo methodInfo = superMapping.GetMethod("Configure");
                var modelBuilderType = modelBuilder.GetType();
                //var modelBuilderMethods = modelBuilderType.GetMethods();
                //var modelBuilderEntityMethodInfos = modelBuilderMethods.Where(n => n.Name == "Entity").Where(n => n.IsGenericMethod).ToList();
                //var modelBuilderEntityMethodInfo = modelBuilderEntityMethodInfos.FirstOrDefault(n => n.GetParameters().Length == 0);
                //modelBuilderEntityMethodInfo = modelBuilderEntityMethodInfo.MakeGenericMethod(genericArguments);
                //var p = modelBuilderEntityMethodInfo.Invoke(modelBuilder, null);
                //methodInfo.Invoke(mapping, null);


                MethodInfo modelBuilderApplyConfigurationMethodInfo = modelBuilderType.GetMethod("ApplyConfiguration");
                modelBuilderApplyConfigurationMethodInfo = modelBuilderApplyConfigurationMethodInfo.MakeGenericMethod(genericArguments);
                modelBuilderApplyConfigurationMethodInfo.Invoke(modelBuilder, new object[] { mapping });
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
