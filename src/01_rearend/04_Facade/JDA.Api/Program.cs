using JDA.Core.DI;
using JDA.Core.Persistence.Extensions;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Extensions.Primitives;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using JDA.Core.Persistence.Contexts.Default;
using System.Reflection;
using JDA.Core.WebApi.ApiDocs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using JDA.Core.WebApi.Filters;
using JDA.Core.WebApi.MiddleWares;
using Serilog;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JDA.Core.WebApi.HttpLoggings;

try
{
    var builder = WebApplication.CreateBuilder(args);

    Serilog.Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    Log.Information("Starting webapi application");

    builder.Host.UseSerilog(dispose: true); // <-- Add this line

    // Add services to the container.

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add(typeof(ApiActionFilter));
        options.Filters.Add(typeof(ApiExceptionFilter));
    })
        .AddNewtonsoftJson(options =>
        {
        // 指定如何解决循环引用：
        //1、Ignore将忽略循环引用
        //2、Serialize将序列化循环引用
        //3、Error将抛出异常
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        // 统一设置API的日期格式
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
        //// 统一设置JSON内实体的格式（默认JSON里的首字母为小写，这里改为同后端Mode一致）
        //options.SerializerSettings.ContractResolver = new DefaultContractResolver();//设置JSON返回格式同model一致
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();//设置JSON返回格式同model一致
        });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddDatabase<JDADbContext>(builder.Configuration);
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<ITenant>(sp =>
    {
        var tenantIdString = sp.GetRequiredService<IHttpContextAccessor>().HttpContext?.Request?.Query?["TenantId"];
        if (tenantIdString is null) return null;

        return tenantIdString.Value != StringValues.Empty && int.TryParse(tenantIdString, out var tenantId)
            ? new Tenant(tenantId)
        : null;
    });

    builder.Services.AddDependencyInjectionService();
    //builder.Services.AddAutoMapper(typeof(ViewModelProfile));
    builder.Services.AddHttpLoggingHandlerWorker();

    builder.Services.AddSwaggerGen(options =>
    {
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
    });

    var app = builder.Build();
    app.UseHttpLogging();
    app.UseLoggerScope();
    app.UseException();
    //app.UseHappenException();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
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
    }

    app.UseStaticFiles();
    //app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.Information("Stop webapi application");
    Log.CloseAndFlush();
}

public class Tenant : ITenant
{
    public Tenant(int tenantId)
    {
        TenantId = $"{tenantId}";
    }
    public string TenantId { get; set; }

    public string Key => "a";

    public TenantIdDescription Inner => new TenantIdDescription();

}