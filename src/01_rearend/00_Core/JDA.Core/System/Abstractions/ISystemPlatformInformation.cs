using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JDA.Core.SystemOperate.Abstractions
{
    /// <summary>
    /// 系统运行平台信息
    /// </summary>
    public interface ISystemPlatformInformation
    {
        /// <summary>
        /// 运行框架
        /// </summary>
        [Description("运行框架")]
        public string FrameworkDescription { get; }
        /// <summary>
        /// 操作系统
        /// </summary>
        [Description("操作系统")]
        public string OSDescription { get; }
        /// <summary>
        /// 操作系统版本
        /// </summary>
        [Description("操作系统版本")]
        public OperatingSystem OSVersion { get; }
        /// <summary>
        /// 平台架构
        /// </summary>
        [Description("平台架构")]
        public Architecture OSArchitecture { get; }
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
    }
}
