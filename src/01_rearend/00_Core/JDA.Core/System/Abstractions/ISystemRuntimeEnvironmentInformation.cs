using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JDA.Core.System.Abstractions
{
    /// <summary>
    /// 系统运行环境信息
    /// </summary>
    public interface ISystemRuntimeEnvironmentInformation
    {
        /// <summary>
        /// 机器名称
        /// </summary>
        [Description("机器名称")]
        public IMachineInformation MachineInformation { get; }
        /// <summary>
        /// 用户网络域名
        /// </summary>
        [Description("用户网络域名")]
        public string UserDomainName { get; }
        /// <summary>
        /// 分区磁盘
        /// </summary>
        [Description("分区磁盘")]
        public string GetLogicalDrives { get; }
        /// <summary>
        /// 系统目录
        /// </summary>
        [Description("系统目录")]
        public string SystemDirectory { get; }
        /// <summary>
        /// 系统已运行时间(毫秒)
        /// </summary>
        [Description("系统已运行时间(毫秒)")]
        public int TickCount { get; }
        /// <summary>
        /// 是否在交互模式中运行
        /// </summary>
        [Description("是否在交互模式中运行")]
        public bool UserInteractive { get; }
        /// <summary>
        /// 当前关联用户名
        /// </summary>
        [Description("当前关联用户名")]
        public string UserName { get; }
        /// <summary>
        /// Web程序核心框架版本
        /// </summary>
        [Description("Web程序核心框架版本")]
        public Version Version { get; }

        /// <summary>
        /// 磁盘分区 Linux中无效
        /// </summary>
        [Description("磁盘分区")]
        public string SystemDrive { get; }
        /// <summary>
        /// 系统目录 Linux中无效
        /// </summary>
        [Description("系统目录")]
        public string SystemRoot { get; }
    }
}
