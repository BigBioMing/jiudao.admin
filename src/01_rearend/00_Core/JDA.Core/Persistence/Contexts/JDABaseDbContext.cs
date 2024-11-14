using JDA.Core.Models.ApiModelErrors;
using JDA.Core.Persistence.Maps;
using JDA.Core.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

            #region MyRegion
            #region 方式一
            {
                //// 1. 直接创建对象修改到数据库 不用先查询
                //t_user user = new t_user()
                //{
                //    id = id,
                //    name = "周",
                //    age = 1,

                //};

                //// 2. 将实体对象加入 EF 对象容器中 获取容器对象
                //DbEntityEntry<t_user> entry = DbContent.Entry<t_user>(user);

                //// 3. 容器对象状态设置为 unchanged
                //entry.State = System.Data.EntityState.Unchanged;

                //// 4. 设置被改变的属性  是否要提交到数据库的字段
                //entry.Property(a => a.name).IsModified = true;
                //entry.Property(a => a.age).IsModified = true;

                //// 5. 更新数据
                //DbContent.SaveChanges();
            }
            #endregion
            #region 方式二
            {
                //// 1. 先查询实体
                //var user = DbContent.t_user.Where(o => o.id == id).FirstOrDefault();

                //// 2. 再修改字段的值
                //user.name = "周";
                //user.age = 1;

                //// 3. age 属性不想修改，标记其 IsModified 属性 = false
                //// ---- 设置容器空间某一个模型的某一个字段 不提交到数据库
                //// ---- DbContent.Entry 是要更新到数据库的整个对象

                //DbContent.Entry<t_user>(user).Property("age").IsModified = false;

                //// 4. 更新数据
                //DbContent.SaveChanges();
            }
            #endregion
            #region 方式三
            {
                //// 假设有一个实体的实例entity，你想要更新其Name字段
                //var entity = new YourEntity { Id = 1, Name = "新名称" };

                //// 方法1: 使用DbSet.Attach
                //context.YourEntities.Attach(entity);
                //context.Entry(entity).Property(e => e.Name).IsModified = true;
                //context.SaveChanges();

                //// 方法2: 使用DbContext.Entry
                //var entry = context.Entry(entity);
                //entry.State = EntityState.Unchanged; // 先将状态设置为Unchanged
                //entry.Property(e => e.Name).IsModified = true; // 标记Name字段为修改过
                //context.SaveChanges();
            }
            #endregion
            #endregion
        }

        public int TenantId { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 通过反射获取所有继承了 SuperMapping<> 的类，并将其注册到EF中
            //mapping超类
            Type superMapping = typeof(SuperMapping<>);
            //var assembly = Assembly.GetEntryAssembly();
            List<Assembly> assemblies = AssemblyHelper.GetAssemblies();

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
                //执行 modelBuilder.ApplyConfiguration 方法，将 mappingInstance 注册到EF中
                modelBuilderApplyConfigurationMethodInfo.Invoke(modelBuilder, new object[] { mappingInstance });
            }
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
