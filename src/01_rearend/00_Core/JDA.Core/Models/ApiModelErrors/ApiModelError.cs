using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Models.ApiModelErrors
{
    /// <summary>
    /// 接口参数模型验证信息（错误信息）
    /// </summary>
    public class ApiModelError
    {
        /// <summary>
        /// 参数字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
    }
}
