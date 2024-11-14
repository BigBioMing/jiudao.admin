using JDA.Core.SystemOperate.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.SystemOperate.Implements
{
    /// <summary>
    /// 应用程序运行信息
    /// </summary>
    [Description("应用程序运行信息")]
    public class DefaultApplicationRuntimeInformation : IApplicationRuntimeInformation
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
        public int ProcessorCount { get => Environment.ProcessorCount; }
        /// <summary>
        /// 进程已使用物理内存(kb)
        /// </summary>
        [Description("进程已使用物理内存(kb)")]
        public decimal UsedMemory { get => Convert.ToDecimal(Process.GetCurrentProcess().WorkingSet64 / 1024.0); }
        /// <summary>
        /// 进程已占耗CPU时间(ms)
        /// </summary>
        [Description("进程已占耗CPU时间(ms)")]
        public double UsedCPUTime { get => Process.GetCurrentProcess().TotalProcessorTime.TotalMilliseconds; }
    }
}
