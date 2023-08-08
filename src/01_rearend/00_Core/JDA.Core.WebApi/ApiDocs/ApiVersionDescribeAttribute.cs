using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.ApiDocs
{
    /// <summary>
    /// API版本描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ApiVersionDescribeAttribute : Attribute
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
