using Microsoft.Azure.Management.ResourceManager.Fluent.Core.DAG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Formats.WebApi
{
    /// <summary>
    /// 统一响应格式
    /// </summary>
    public class UnifyResponse<TData>
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回值
        /// </summary>
        public TData Data { get; set; }

        public UnifyResponse()
        {
        }

        public UnifyResponse(string code, string? message = null, TData? data = default)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }

        public static UnifyResponse<TData> Success(string? message = null, TData? data = default)
        {
            return new UnifyResponse<TData>()
            {
                Code = "200",
                Message = message,
                Data = data
            };
        }

        public static UnifyResponse<TData> Error(string? message = null, TData? data = default)
        {
            return new UnifyResponse<TData>()
            {
                Code = "500",
                Message = message,
                Data = data
            };
        }

        public static UnifyResponse<TData> ErrorCustomCode(string code, string? message = null, TData? data = default)
        {
            return new UnifyResponse<TData>()
            {
                Code = code,
                Message = message,
                Data = data
            };
        }
    }
}
