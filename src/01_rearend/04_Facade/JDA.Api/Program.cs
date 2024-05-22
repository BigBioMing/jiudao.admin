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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using JDA.Core.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

try
{
    var builder = WebApplication.CreateBuilder(args);

    //var configuration = new ConfigurationBuilder()
    //    .SetBasePath(Directory.GetCurrentDirectory())
    //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    //    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
    //    .AddEnvironmentVariables()
    //    .Build();

    Serilog.Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    Log.Information("Starting webapi application");

    builder.Host.UseSerilog(dispose: true); // <-- Add this line

    AppSettingsHelper.Initialize(builder.Configuration);

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

    #region JWT
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        string jwtKey = builder.Configuration["Jwt:IssuerSigningKey"];
        if (string.IsNullOrWhiteSpace(jwtKey)) throw new Exception("Jwt密钥未配置");
        string jwtIssuer = builder.Configuration["Jwt:Issuer"];
        if (string.IsNullOrWhiteSpace(jwtIssuer)) throw new Exception("Jwt:Issuer未配置");
        string jwtAudience = builder.Configuration["Jwt:Audience"];
        if (string.IsNullOrWhiteSpace(jwtAudience)) throw new Exception("Jwt:Audience未配置");

        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)), // 配置你的密钥
            ValidateIssuer = true, //是否验证Issuer
            ValidIssuer = jwtIssuer,//签发人
            ValidateAudience = true, //是否验证Audience
            ValidAudience = jwtAudience,//受众
            ValidateLifetime = true, //是否验证失效时间
            ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
            RequireExpirationTime = true,
            // 其他可选参数
        };
    });
    #endregion

    var app = builder.Build();
    app.UseHttpLogging();
    app.UseLoggerScope();
    app.UseException();
    //app.UseHappenException();
    //身份验证
    app.UseAuthentication();

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
    //app.UseHttpsRedirection();
    //授权
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