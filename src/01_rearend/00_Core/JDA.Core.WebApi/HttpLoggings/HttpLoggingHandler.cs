using JDA.Core.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace JDA.Core.WebApi.HttpLoggings
{
    /// <summary>
    /// 请求日志处理类
    /// </summary>
    public class HttpLoggingHandler
    {
        private static ConcurrentQueue<HttpLoggingInformation> _queue = new ConcurrentQueue<HttpLoggingInformation>();

        public static void AddLog(HttpLoggingInformation loggingInformation)
        {
            _queue.Enqueue(loggingInformation);
        }

        public static async Task Handler(IServiceProvider _serviceProvider)
        {
            List<HttpLoggingInformation> list = new List<HttpLoggingInformation>();
            while (true)
            {
                //Log.Information($"开始写入第三方调用日志");
                using (var scope = _serviceProvider.CreateScope())
                {
                    DateTime now = DateTime.Now;
                    //long timestamp = DateTimeHelper.ExtractDateTimeToUnixTimestamp(now);
                    //var currentUser = (scope.ServiceProvider.GetService(typeof(CurrentUser)) as CurrentUser);
                    //currentUser.LoginName = $"api_{Environment.MachineName}";
                    //currentUser.RequestTimestamp = timestamp;
                    //currentUser.RequestDate = now;
                    //currentUser.RequestId = Guid.NewGuid().ToString("N");
                    //var logger = (scope.ServiceProvider.GetService(typeof(ILogger<CallThirdApiLogWorker>)) as ILogger<CallThirdApiLogWorker>);
                    var logger = scope.ServiceProvider.GetService<ILogger<HttpLoggingHandler>>();

                    using (logger.BeginScope("ScopeId:{CurrentScopeId}", Guid.NewGuid().ToString("N")))
                    {
                        //Log.Information($"开始写入第三方调用日志2");
                        if (_queue.Count > 0 && _queue.Count < 500)
                        {
                            _queue.TryDequeue(out HttpLoggingInformation log);
                            list.Add(log);
                            continue;
                        }

                        await Task.Delay(1000);

                        if (list.Count == 0) continue;

                        try
                        {
                            Log.Information($"写入http请求日志批量数据");
                            await scope.ServiceProvider.GetRequiredService<IHttpLoggingStorage>().SaveAsync(list);
                            Log.Information($"完成写入http请求日志批量数据");
                            list.Clear();
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                Log.Error(ex, $"http请求日志批量写入数据时出错{JsonConvert.SerializeObject(list)}");
                                Log.Information($"将日志重新入队");
                                foreach (var item in list)
                                {
                                    _queue.Enqueue(item);
                                }
                                Log.Information($"日志重新入队成功");
                                list.Clear();
                            }
                            catch (Exception e)
                            {
                                Log.Error(e, $"重新入队时出错");
                                list.Clear();
                            }

                        }
                    }
                }
            }
        }
    }
}
