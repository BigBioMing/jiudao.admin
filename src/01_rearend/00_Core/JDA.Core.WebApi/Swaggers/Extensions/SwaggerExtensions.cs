using JDA.Core.WebApi.ApiDocs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.Swaggers.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddJdaSwagger(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SchemaFilter<DefaultValueSchemaFilter>();
                var fields = typeof(ApiVersionDefine).GetFields();
                fields = fields.Skip(1).ToArray();
                foreach (FieldInfo field in fields)
                {
                    ApiVersionDescribeAttribute describe = field.GetCustomAttribute<ApiVersionDescribeAttribute>();
                    options.SwaggerDoc(describe.Version, new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = $"{describe.Version}：{describe.Name}",
                        Version = describe.Version,
                        Description = describe.Name
                    });
                }
                //没有特性的分到NoGroup分组
                options.SwaggerDoc("NoGroup", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "无分组"
                });
                //判断接口在哪一个分组
                options.DocInclusionPredicate((docName, apiDescription) =>
                {
                    if (docName == "NoGroup")
                    {
                        return string.IsNullOrEmpty(apiDescription.GroupName);
                    }
                    else
                    {
                        return apiDescription.GroupName == docName;
                    }
                });

                {
                    var file = Path.Combine(AppContext.BaseDirectory, "JDA.Model.xml");  // xml文档绝对路径
                    var path = Path.Combine(AppContext.BaseDirectory, file); // xml文档绝对路径
                    options.IncludeXmlComments(path, true); // true : 显示控制器层注释
                }
                {
                    var file = Path.Combine(AppContext.BaseDirectory, "JDA.Entity.xml");  // xml文档绝对路径
                    var path = Path.Combine(AppContext.BaseDirectory, file); // xml文档绝对路径
                    options.IncludeXmlComments(path, true); // true : 显示控制器层注释
                }
                {
                    var file = Path.Combine(AppContext.BaseDirectory, "JDA.Api.xml");  // xml文档绝对路径
                    var path = Path.Combine(AppContext.BaseDirectory, file); // xml文档绝对路径
                    options.IncludeXmlComments(path, true); // true : 显示控制器层注释
                }

                options.OrderActionsBy(o => o.RelativePath); // 对action的名称进行排序，如果有多个，就可以看见效果了。

                #region 启用swagger验证功能
                //添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称一致即可。
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 在下方输入Bearer {token} 即可，注意两者之间有空格",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",

                });
                #endregion
            });


            return services;
        }

        public static IApplicationBuilder UseJdaSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var fields = typeof(ApiVersionDefine).GetFields();
                fields = fields.Skip(1).ToArray();
                foreach (FieldInfo field in fields)
                {
                    ApiVersionDescribeAttribute describe = field.GetCustomAttribute<ApiVersionDescribeAttribute>();
                    options.SwaggerEndpoint($"/swagger/{field.Name}/swagger.json", $"{describe.Name}");
                }
                options.SwaggerEndpoint("/swagger/NoGroup/swagger.json", "无分组");
            });

            return app;
        }
    }
}
