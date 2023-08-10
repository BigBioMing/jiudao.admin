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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDatabase<JDADbContext>(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenant>(sp =>
{
    var tenantIdString = sp.GetRequiredService<IHttpContextAccessor>().HttpContext.Request.Query["TenantId"];

    return tenantIdString != StringValues.Empty && int.TryParse(tenantIdString, out var tenantId)
        ? new Tenant(tenantId)
    : null;
});

builder.Services.AddDependencyInjectionService();
//builder.Services.AddAutoMapper(typeof(ViewModelProfile));

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
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

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