using Microsoft.Azure.Management.ResourceManager.Fluent.Core.DAG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Exceptions
{
    /// <summary>
    /// 业务异常
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(string? message) : base(message)
        {
            Code = "99999";//通用错误码
        }
        public BusinessException(string code, string? message) : base(message)
        {
            Code = code;
        }

        public string Code { get; set; }
    }
}
