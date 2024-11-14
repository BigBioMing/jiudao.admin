using JDA.Core.SystemOperate.Abstractions;
using JDA.Core.Users.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Users.Implements
{
    /// <summary>
    /// 程序当前运行上下文
    /// </summary>
    public class DefaultCurrentRunningContext : ICurrentRunningContext
    {
        private readonly IMachineInformation _machineInformation;
        public DefaultCurrentRunningContext(IMachineInformation machineInformation)
        {
            this._machineInformation = machineInformation;
        }

        /// <summary>
        /// 当前操作人ID
        /// </summary>
        public long UserId { get => 0; }
        /// <summary>
        /// 当前操作人名称
        /// </summary>
        public string UserName { get => this._machineInformation.MachineName; }
    }
}
