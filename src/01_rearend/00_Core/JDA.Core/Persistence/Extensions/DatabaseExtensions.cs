using JDA.Core.Persistence.Contexts;
using JDA.Core.Persistence.DbContextFactories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Extensions
{
    /// <summary>
    /// 数据库注册服务
    /// </summary>
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase<TDbContext>(this IServiceCollection services, IConfiguration configuration) where TDbContext : JDABaseDbContext
        {
            // 数据库类型
            string dbType = configuration["Connection:DbType"];
            // 数据库链接字符串.
            string connectionString = configuration["Connection:DbConnectionString"];

            if (string.IsNullOrWhiteSpace(dbType))
                throw new Exception("请预制数据库类型");

            // mysql
            if (string.Compare(dbType, "mysql", StringComparison.OrdinalIgnoreCase) == 0)
            {
                // Replace with your server version and type.
                // Use 'MariaDbServerVersion' for MariaDB.
                // Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
                // For common usages, see pull request #1233.
                var serverVersion = new MySqlServerVersion(new Version(5, 7, 36));

                bool enableDbContextPool = true;
                if (!enableDbContextPool)
                {
                    // Replace 'YourDbContext' with the name of your own DbContext derived class.
                    services.AddDbContext<TDbContext>(
                        dbContextOptions => dbContextOptions
                            .UseMySql(connectionString, serverVersion)
                            // The following three options help with debugging, but should
                            // be changed or removed for production.
                            .LogTo(Console.WriteLine, LogLevel.Information)
                            .EnableSensitiveDataLogging()
                            .EnableDetailedErrors()
                    );
                }
                else
                {
                    // 方式一
                    //// 上下文池
                    //services.AddDbContextPool<TDbContext>(
                    //    dbContextOptions => dbContextOptions
                    //        .UseMySql(connectionString, serverVersion)
                    //        // The following three options help with debugging, but should
                    //        // be changed or removed for production.
                    //        .LogTo(Console.WriteLine, LogLevel.Information)
                    //        .EnableSensitiveDataLogging()
                    //        .EnableDetailedErrors()
                    //);

                    // 方式二 在提供上下文时使用工厂修改随请求改变的状态
                    // 池上下文工厂注册为单一实例服务
                    services.AddPooledDbContextFactory<TDbContext>(
                        dbContextOptions => dbContextOptions
                            .UseMySql(connectionString, serverVersion)
                            // The following three options help with debugging, but should
                            // be changed or removed for production.
                            .LogTo(Console.WriteLine, LogLevel.Information)
                            .EnableSensitiveDataLogging()
                            .EnableDetailedErrors()
                    );
                    // 将自定义的上下文工厂注册为作用域服务
                    services.AddScoped<JDADefaultDbContextFactory<TDbContext>>();
                    // 注册要从作用域工厂注入的上下文
                    services.AddScoped(sp => sp.GetRequiredService<JDADefaultDbContextFactory<TDbContext>>().CreateDbContext());
                }
            }

            return services;
        }
    }
}
