using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.ApiDocs
{
    /// <summary>
    /// API版本
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ApiVersionAttribute : Attribute
    {
        public ApiVersionzDefine Version { get; set; }
    }
}
