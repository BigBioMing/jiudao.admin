using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JDA.Core.SystemOperate.Abstractions
{
    /// <summary>
    /// 应用程序运行信息
    /// </summary>
    public interface IApplicationRuntimeInformation
    {
        /// <summary>
        /// 最大内存(kb)
        /// </summary>
        [Description("最大内存(kb)")]
        public decimal MaximumMemory { get; }
        /// <summary>
        /// 逻辑处理器数量
        /// </summary>
        [Description("逻辑处理器数量")]
        public int ProcessorCount { get; }
        /// <summary>
        /// 进程已使用物理内存(kb)
        /// </summary>
        [Description("进程已使用物理内存(kb)")]
        public decimal UsedMemory { get; }
        /// <summary>
        /// 进程已占耗CPU时间(ms)
        /// </summary>
        [Description("进程已占耗CPU时间(ms)")]
        public double UsedCPUTime { get; }
    }
}
