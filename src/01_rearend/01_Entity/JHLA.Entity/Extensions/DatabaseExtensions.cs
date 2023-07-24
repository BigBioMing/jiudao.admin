using JHLA.Entity.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHLA.Entity.Extensions
{
    /// <summary>
    /// 数据库注册服务
    /// </summary>
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
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

                // Replace 'YourDbContext' with the name of your own DbContext derived class.
                services.AddDbContext<JHLADbContext>(
                    dbContextOptions => dbContextOptions
                        .UseMySql(connectionString, serverVersion)
                        // The following three options help with debugging, but should
                        // be changed or removed for production.
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors()
                );
            }

            return services;
        }
    }
}
