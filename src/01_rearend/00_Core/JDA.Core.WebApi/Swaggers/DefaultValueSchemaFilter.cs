using JDA.Core.Loggers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.Swaggers
{
    /// <summary>
    /// 在swagger上实现参数默认值
    /// </summary>
    public class DefaultValueSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            try
            {
                //if (!context.Type.IsClass || context.Type == typeof(string) || !context.Type.IsPublic || context.Type.IsArray) return;
                if (!context.Type.IsClass || context.Type == typeof(string) || context.Type.IsArray) return;
                var obj = Activator.CreateInstance(context.Type);
                //_ = (from sc in schema.Properties
                //     join co in context.Type.GetProperties() on sc.Key.ToLower() equals co.Name.ToLower()
                //     select sc.Value.Example = co.GetValue(obj) != null ? CreateSchema(sc.Value, co,  obj).Example : sc.Value.Example).ToList();

                var list = (from sc in schema.Properties
                            join co in context.Type.GetProperties() on sc.Key.ToLower() equals co.Name.ToLower()
                            select new { OpenApiSchema = sc.Value, Property = co }).ToList();
                foreach (var item in list)
                {
                    if (item.OpenApiSchema?.Type == "string")
                    {
                        string? val = item.Property?.GetValue(obj)?.ToString();
                        if (item.OpenApiSchema != null && val != null)
                        {
                            item.OpenApiSchema.Default = new OpenApiString(val);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LoggerHelper.Default.Error(e);
                throw;
            }


            //if (schema == null)
            //{
            //    return;
            //}
            //var objectSchema = schema;
            //foreach (var property in objectSchema.Properties)
            //{
            //    //设置字符串类型参数为""值
            //    if (property.Value.Type == "string" && property.Value.Default == null)
            //    {
            //        property.Value.Default = new OpenApiString("");
            //    }
            //    //通过属性特性赋值默认值
            //    DefaultValueAttribute? defaultValueAttribute = context.ParameterInfo?.GetCustomAttribute<DefaultValueAttribute>();
            //    if (defaultValueAttribute != null)
            //    {
            //        property.Value.Example = (IOpenApiAny)defaultValueAttribute.Value;
            //    }
            //}
        }

        OpenApiSchema CreateSchema(OpenApiSchema openApiSchema, PropertyInfo propertyInfo, object obj)
        {
            string? val = propertyInfo.GetValue(obj)?.ToString();
            openApiSchema.Default = new OpenApiString(val);
            //document.Components = new OpenApiComponents();
            //document.Components.Schemas.Add("someSchema", schema);

            return openApiSchema;
        }
    }
}
