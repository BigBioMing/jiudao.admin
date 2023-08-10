using Microsoft.AspNetCore.Mvc.ApiExplorer;
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
    public class ApiVersionAttribute : Attribute, IApiDescriptionGroupNameProvider, IApiDescriptionVisibilityProvider
    {
        private ApiVersionDefine _version;
        public ApiVersionAttribute()
        {
            this.IgnoreApi = false;
        }
        public string GroupName { get; set; }
        public ApiVersionDefine Version
        {
            get => this._version;
            set
            {
                this._version = value;
                this.GroupName = this._version.ToString();
            }
        }

        public bool IgnoreApi { get; set; }
    }
}
