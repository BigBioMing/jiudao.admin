using JDA.Core.SystemOperate.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.SystemOperate.Implements
{
    /// <summary>
    /// 机器信息
    /// </summary>
    [Description("机器信息")]
    public class DefaultMachineInformation : IMachineInformation
    {
        /// <summary>
        /// 机器名称
        /// </summary>
        [Description("机器名称")]
        public string MachineName { get => Environment.MachineName; }
    }
}
