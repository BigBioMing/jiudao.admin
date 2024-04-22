using JDA.Core.Attributes;
using JDA.Core.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Entity.Entities.Sys
{
    /// <summary>
    /// HTTP请求响应日志表
    /// </summary>
    public partial class SysLog : SuperEntity
    {
        /// <summary>
        /// Host
        /// </summary>
        [Display(Name = "Host", Order = 1)]
        [ColumnMetadata(Name = "Host", Order = 1)]
        public string Host { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        [Display(Name = "Url", Order = 1)]
        [ColumnMetadata(Name = "Url", Order = 1)]
        public string Url { get; set; }
        /// <summary>
        /// 请求谓词
        /// </summary>
        [Display(Name = "请求谓词", Order = 2)]
        [ColumnMetadata(Name = "请求谓词", Order = 2)]
        public string Method { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间", Order = 3)]
        [ColumnMetadata(Name = "开始时间", Order = 3)]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间", Order = 4)]
        [ColumnMetadata(Name = "结束时间", Order = 4)]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 开始时间戳
        /// </summary>
        [Display(Name = "开始时间戳", Order = 4)]
        [ColumnMetadata(Name = "开始时间戳", Order = 4)]
        public long StartTimestamp { get; set; }
        /// <summary>
        /// 结束时间戳
        /// </summary>
        [Display(Name = "结束时间戳", Order = 4)]
        [ColumnMetadata(Name = "结束时间戳", Order = 4)]
        public long EndTimestamp { get; set; }
        /// <summary>
        /// 请求耗时
        /// </summary>
        [Display(Name = "请求耗时", Order = 5)]
        [ColumnMetadata(Name = "请求耗时", Order = 5)]
        public decimal Cost { get; set; }
        /// <summary>
        /// Headers
        /// </summary>
        [Display(Name = "Headers", Order = 6)]
        [ColumnMetadata(Name = "Headers", Order = 6)]
        public string Headers { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        [Display(Name = "请求参数", Order = 7)]
        [ColumnMetadata(Name = "请求参数", Order = 7)]
        public string RequestParams { get; set; }
        /// <summary>
        /// 响应参数
        /// </summary>
        [Display(Name = "响应参数", Order = 8)]
        [ColumnMetadata(Name = "响应参数", Order = 8)]
        public string ResponseParams { get; set; }
    }
}
