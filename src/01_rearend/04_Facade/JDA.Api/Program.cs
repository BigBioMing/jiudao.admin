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
using JDA.Core.WebApi.Authorizations;
using Microsoft.AspNetCore.Authorization;
using JDA.Core.WebApi.Swaggers.Extensions;
using Microsoft.AspNetCore.Mvc;

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
        //options.Filters.Add(typeof(ApiActionFilter));
        options.Filters.Add(new ApiActionFilter()); //注册全全局模型验证过滤器
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
    //关闭自动验证 -- 模型验证
    builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
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

    // 注册自定义策略处理程序
    builder.Services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
    #region JWT
    builder.Services.AddAuthorization(options =>
    {
        // 自定义基于策略的授权权限
        options.AddPolicy("Permission",
                 policy => policy.Requirements.Add(new CustomRequirement()));
    })
    .AddAuthentication(x =>
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

    builder.Services.AddJdaSwagger();



    //跨域
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("MyCorsPolicy",
            builder =>
            {
                builder
                       //.WithOrigins("http://example.com")
                       .AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
    });

    var app = builder.Build();
    app.UseCors("MyCorsPolicy");
    app.UseHttpLogging();
    app.UseLoggerScope();
    app.UseException();
    //app.UseHappenException();
    //身份验证
    app.UseAuthentication();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseJdaSwagger();
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