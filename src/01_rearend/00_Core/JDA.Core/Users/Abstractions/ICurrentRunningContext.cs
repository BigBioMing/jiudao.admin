using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Users.Abstractions
{
    /// <summary>
    /// 程序当前运行上下文
    /// </summary>
    public interface ICurrentRunningContext
    {
        /// <summary>
        /// 当前操作人ID
        /// </summary>
        public long UserId { get; }
        /// <summary>
        /// 当前操作人名称
        /// </summary>
        public string UserName { get; }
    }
}
