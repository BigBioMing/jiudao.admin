using JDA.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.HttpLoggings
{
    /// <summary>
    /// HTTP请求响应日志信息
    /// </summary>
    public class HttpLoggingInformation
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 请求ID
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// Host
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 请求谓词
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 开始时间戳
        /// </summary>
        public long StartTimestamp { get; set; }
        /// <summary>
        /// 结束时间戳
        /// </summary>
        public long EndTimestamp { get; set; }
        /// <summary>
        /// 请求耗时
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Headers
        /// </summary>
        public string? Headers { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public string? RequestParams { get; set; }
        /// <summary>
        /// 响应参数
        /// </summary>
        public string? ResponseParams { get; set; }
    }
}
