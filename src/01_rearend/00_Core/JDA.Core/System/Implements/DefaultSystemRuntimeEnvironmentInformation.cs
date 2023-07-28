using JDA.Core.System.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.System.Implements
{
    /// <summary>
    /// 系统运行环境信息
    /// </summary>
    [Description("系统运行环境信息")]
    public class DefaultSystemRuntimeEnvironmentInformation : ISystemRuntimeEnvironmentInformation
    {
        private readonly IMachineInformation _machineInformation;
        public DefaultSystemRuntimeEnvironmentInformation(IMachineInformation machineInformation)
        {
            this._machineInformation = machineInformation;
        }

        /// <summary>
        /// 机器信息
        /// </summary>
        [Description("机器信息")]
        public IMachineInformation MachineInformation { get => this._machineInformation; }
        /// <summary>
        /// 用户网络域名
        /// </summary>
        [Description("用户网络域名")]
        public string UserDomainName { get { return Environment.UserDomainName; } }
        /// <summary>
        /// 分区磁盘
        /// </summary>
        [Description("分区磁盘")]
        public string GetLogicalDrives { get { return string.Join(", ", Environment.GetLogicalDrives()); } }
        /// <summary>
        /// 系统目录
        /// </summary>
        [Description("系统目录")]
        public string SystemDirectory { get { return Environment.SystemDirectory; } }
        /// <summary>
        /// 系统已运行时间(毫秒)
        /// </summary>
        [Description("系统已运行时间(毫秒)")]
        public int TickCount { get { return Environment.TickCount; } }
        /// <summary>
        /// 是否在交互模式中运行
        /// </summary>
        [Description("是否在交互模式中运行")]
        public bool UserInteractive { get { return Environment.UserInteractive; } }
        /// <summary>
        /// 当前关联用户名
        /// </summary>
        [Description("当前关联用户名")]
        public string UserName { get { return Environment.UserName; } }
        /// <summary>
        /// Web程序核心框架版本
        /// </summary>
        [Description("Web程序核心框架版本")]
        public Version Version { get { return Environment.Version; } }

        /// <summary>
        /// 磁盘分区 Linux中无效
        /// </summary>
        [Description("磁盘分区")]
        public string SystemDrive { get { return Environment.ExpandEnvironmentVariables("%SystemDrive%"); } }
        /// <summary>
        /// 系统目录 Linux中无效
        /// </summary>
        [Description("系统目录")]
        public string SystemRoot { get { return Environment.ExpandEnvironmentVariables("%SystemRoot%"); } }
    }
}
