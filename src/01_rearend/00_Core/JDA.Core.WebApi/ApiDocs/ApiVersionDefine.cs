using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.ApiDocs
{
    /// <summary>
    /// API版本定义
    /// </summary>
    public enum ApiVersionDefine
    {
        [Description("V1")]
        [ApiVersionDescribe(Name = "V1", Version = "V1")]
        V1 = 1,
        [Description("V2")]
        [ApiVersionDescribe(Name = "V2", Version = "V2")]
        V2 = 2,
    }
}
