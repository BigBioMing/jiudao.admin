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
        options.Filters.Add(new ApiActionFilter()); //ע��ȫȫ��ģ����֤������
        options.Filters.Add(typeof(ApiExceptionFilter));
    })
        .AddNewtonsoftJson(options =>
        {
            // ָ����ν��ѭ�����ã�
            //1��Ignore������ѭ������
            //2��Serialize�����л�ѭ������
            //3��Error���׳��쳣
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            // ͳһ����API�����ڸ�ʽ
            options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            //// ͳһ����JSON��ʵ��ĸ�ʽ��Ĭ��JSON�������ĸΪСд�������Ϊͬ���Modeһ�£�
            //options.SerializerSettings.ContractResolver = new DefaultContractResolver();//����JSON���ظ�ʽͬmodelһ��
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();//����JSON���ظ�ʽͬmodelһ��
        });
    //�ر��Զ���֤ -- ģ����֤
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

    // ע���Զ�����Դ������
    builder.Services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
    #region JWT
    builder.Services.AddAuthorization(options =>
    {
        // �Զ�����ڲ��Ե���ȨȨ��
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
        if (string.IsNullOrWhiteSpace(jwtKey)) throw new Exception("Jwt��Կδ����");
        string jwtIssuer = builder.Configuration["Jwt:Issuer"];
        if (string.IsNullOrWhiteSpace(jwtIssuer)) throw new Exception("Jwt:Issuerδ����");
        string jwtAudience = builder.Configuration["Jwt:Audience"];
        if (string.IsNullOrWhiteSpace(jwtAudience)) throw new Exception("Jwt:Audienceδ����");

        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)), // ���������Կ
            ValidateIssuer = true, //�Ƿ���֤Issuer
            ValidIssuer = jwtIssuer,//ǩ����
            ValidateAudience = true, //�Ƿ���֤Audience
            ValidAudience = jwtAudience,//����
            ValidateLifetime = true, //�Ƿ���֤ʧЧʱ��
            ClockSkew = TimeSpan.FromSeconds(30), //����ʱ���ݴ�ֵ�������������ʱ�䲻ͬ�����⣨�룩
            RequireExpirationTime = true,
            // ������ѡ����
        };
    });
    #endregion

    builder.Services.AddJdaSwagger();



    //����
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
    //�����֤
    app.UseAuthentication();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseJdaSwagger();
    }


    app.UseStaticFiles();
    //app.UseSerilogRequestLogging();
    //app.UseHttpsRedirection();
    //��Ȩ
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