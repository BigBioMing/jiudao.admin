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
                Title = $"{describe.Version}��{describe.Name}",
                Version = describe.Version,
                Description = describe.Name
            });
        }
        //û�����Եķֵ�NoGroup����
        options.SwaggerDoc("NoGroup", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "�޷���"
        });
        //�жϽӿ�����һ������
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
            var file = Path.Combine(AppContext.BaseDirectory, "JDA.Model.xml");  // xml�ĵ�����·��
            var path = Path.Combine(AppContext.BaseDirectory, file); // xml�ĵ�����·��
            options.IncludeXmlComments(path, true); // true : ��ʾ��������ע��
        }
        {
            var file = Path.Combine(AppContext.BaseDirectory, "JDA.Entity.xml");  // xml�ĵ�����·��
            var path = Path.Combine(AppContext.BaseDirectory, file); // xml�ĵ�����·��
            options.IncludeXmlComments(path, true); // true : ��ʾ��������ע��
        }
        {
            var file = Path.Combine(AppContext.BaseDirectory, "JDA.Api.xml");  // xml�ĵ�����·��
            var path = Path.Combine(AppContext.BaseDirectory, file); // xml�ĵ�����·��
            options.IncludeXmlComments(path, true); // true : ��ʾ��������ע��
        }

        options.OrderActionsBy(o => o.RelativePath); // ��action�����ƽ�����������ж�����Ϳ��Կ���Ч���ˡ�

        #region ����swagger��֤����
        //���һ�������ȫ�ְ�ȫ��Ϣ����AddSecurityDefinition����ָ���ķ�������һ�¼��ɡ�
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
            Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ���·�����Bearer {token} ���ɣ�ע������֮���пո�",
            Name = "Authorization",//jwtĬ�ϵĲ�������
            In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
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

    var app = builder.Build();
    app.UseHttpLogging();
    app.UseLoggerScope();
    app.UseException();
    //app.UseHappenException();
    //�����֤
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
         options.SwaggerEndpoint("/swagger/NoGroup/swagger.json", "�޷���");
     });
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