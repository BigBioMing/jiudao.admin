using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Users.Abstractions
{
    /// <summary>
    /// 机器信息
    /// </summary>
    public interface IMachineInformation
    {
        public string HostName { get; }
    }
}
