using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Tokens
{
    public class TokenResult
    {
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 过期时间（单位：分钟）
        /// </summary>
        public int ExpireTime { get; set; }
    }
}
