using JDA.Core.Exceptions;
using JDA.Core.Formats.WebApi;
using JDA.Core.Loggers;
using JDA.Core.WebApi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate next;
        public ExceptionMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public virtual async Task InvokeAsync(HttpContext context, IHttpContextAccessor httpContextAccessor)
        {
            string path = context.Request.Path.ToString();
            if (!path.StartsWith("/api/"))
            {
                await next(context);
            }
            else
            {
                var originalBody = context.Response.Body;
                using (var ms = new MemoryStream())
                {
                    try
                    {
                        // Body赋值为新Body缓存
                        context.Response.Body = ms;
                        // 向下执行（等待返回）
                        await next(context);

                        ms.Seek(0, SeekOrigin.Begin);
                        using var reader = new StreamReader(ms);
                        var str = await reader.ReadToEndAsync();
                        var buffer = Encoding.UTF8.GetBytes(str);

                        var ms2 = new MemoryStream();
                        await ms2.WriteAsync(buffer, 0, buffer.Length);
                        ms2.Seek(0, SeekOrigin.Begin);

                        // 写入到原有的流中
                        await ms2.CopyToAsync(originalBody);
                    }
                    catch (Exception ex)
                    {
                        LoggerHelper.Default.Error(ex, "[2]");
                        ms.Seek(0, SeekOrigin.Begin);
                        //using string responseBody = await new StreamReader(ms).ReadToEndAsync();
                        //var a = JsonConvert.DeserializeObject<UnifyResponse>(responseBody,new JsonSerializerSettings()
                        //{
                        //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        //    // 统一设置API的日期格式
                        //    DateFormatString = "yyyy-MM-dd HH:mm:ss",
                        //    //// 统一设置JSON内实体的格式（默认JSON里的首字母为小写，这里改为同后端Mode一致）
                        //    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();//设置JSON返回格式同model一致
                        //    ContractResolver = new CamelCasePropertyNamesContractResolver()//设置JSON返回格式同model一致
                        //});
                        var res = new UnifyResponse();
                        //判断是否是已知的异常
                        if (ex is BusinessException businessException)
                        {
                            res = new UnifyResponse() { Message = businessException.Message, Code = businessException.Code };
                        }
                        else
                        {
                            context.Response.StatusCode = 500;
                            res = new UnifyResponse() { Message = ex.Message, Code = "50000" };
                            //context.Response.StatusCode = 500;
                        }
                        context.Response.Body = originalBody;
                        await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(res, new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                            // 统一设置API的日期格式
                            DateFormatString = "yyyy-MM-dd HH:mm:ss",
                            //// 统一设置JSON内实体的格式（默认JSON里的首字母为小写，这里改为同后端Mode一致）
                            //options.SerializerSettings.ContractResolver = new DefaultContractResolver();//设置JSON返回格式同model一致
                            ContractResolver = new CamelCasePropertyNamesContractResolver()//设置JSON返回格式同model一致
                        })));
                    }
                }
            }
        }
    }
}
