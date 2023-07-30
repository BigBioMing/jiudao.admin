using JDA.Core.DI;
using JDA.Core.Persistence.Extensions;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Extensions.Primitives;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using Microsoft.EntityFrameworkCore;
using JDA.Core.Persistence.Contexts.Default;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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