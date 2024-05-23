using JDA.Core.WebApi.HttpLoggings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.MiddleWares
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public class HttpLoggingMiddleWare
    {
        private RequestDelegate _next;

        public HttpLoggingMiddleWare(RequestDelegate next)
        {
            this._next = next;
        }

        public virtual async Task InvokeAsync(HttpContext context, IHttpLoggingStorage httpLoggingStorage)
        {
            int startTick = Environment.TickCount;
            DateTime now = DateTime.Now;
            string path = context.Request.Path.ToString();
            if (!path.StartsWith("/api/"))
            {
                await _next(context);
            }
            else
            {
                path += context.Request.QueryString.ToString();
                // 原Body缓存
                Stream originalBody = context.Response.Body;
                try
                {
                    string host = context.Request.Host.ToString();
                    HttpLoggingInformation logInfo = new HttpLoggingInformation()
                    {
                        Host = host,
                        Url = path,
                        Method = context.Request.Method,
                        StartTime = now,
                    };

                    // 开启数据缓存
                    context.Request.EnableBuffering();
                    using (MemoryStream memoryStream = new())
                    {
                        // 复制Body数据到缓存
                        await context.Request.Body.CopyToAsync(memoryStream);
                        context.Request.Body.Position = 0;
                        using (StreamReader streamReader = new(memoryStream))
                        {
                            // 读取Body数据
                            string? body = await streamReader.ReadToEndAsync();
                            logInfo.RequestParams = body;
                        }
                    }

                    // 新Body缓存
                    using (MemoryStream memoryStream = new())
                    {
                        // Body赋值为新Body缓存
                        context.Response.Body = memoryStream;
                        // 向下执行（等待返回）
                        await _next.Invoke(context);
                        // 原Body缓存赋值为新Body缓存
                        memoryStream.Position = 0;
                        await memoryStream.CopyToAsync(originalBody);
                        using (StreamReader streamReader = new(memoryStream))
                        {
                            // 读取Body数据
                            memoryStream.Position = 0;
                            string body = await streamReader.ReadToEndAsync();
                            logInfo.ResponseParams = body;
                        }
                    }
                    var headers = new JObject();
                    var h = context.Request.Headers;
                    var headerKeys = context.Request.Headers.Keys;
                    var ignoreHeaderKeys = new string[2] { "Cookie", "Authorization" };
                    foreach (var key in headerKeys)
                    {
                        if (ignoreHeaderKeys.Contains(key)) continue;
                        var value = h[key].ToString();
                        headers.Add(key, value);
                    }

                    logInfo.Headers = JsonConvert.SerializeObject(headers);
                    logInfo.EndTime = DateTime.Now;
                    logInfo.Cost = Environment.TickCount - startTick;
                    try
                    {
                        //await httpLoggingStorage.SaveAsync(logInfo);
                        HttpLoggingHandler.AddLog(logInfo);
                    }
                    catch (Exception e)
                    {
                        throw;
                        //Log.Error(e, $"保存日志失败[1] {JsonConvert.SerializeObject(logInfo)}");
                    }
                }
                finally
                {
                    // Body重新赋值为原始Body缓存
                    context.Response.Body = originalBody;
                }
            }
        }
    }
}
