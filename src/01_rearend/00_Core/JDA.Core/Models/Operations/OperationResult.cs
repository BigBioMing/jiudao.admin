using JDA.Core.Formats.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Models.Operations
{
    public class OperationResult
    {
        public OperationResult()
        {
            Status = OperationResultStatus.Success;
            Code = "success";
        }

        /// <summary>
        /// 返回代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public OperationResultStatus Status { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }

        #region methods
        public static OperationResult Success()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Code = "success"
            };
        }
        public static OperationResult Success(string code, string? msg = null)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Code = code,
                Message = msg,
            };
        }

        public static OperationResult Error()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Failed,
                Code = "fail"
            };
        }

        public static OperationResult Error(string? msg)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Failed,
                Code = "fail",
                Message = msg,
            };
        }

        public static OperationResult ErrorCustomCode(string code, string? msg = null)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Failed,
                Code = code,
                Message = msg
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UnifyResponse ToResponse()
        {
            if (this.Status != JDA.Core.Models.Operations.OperationResultStatus.Success)
                return UnifyResponse.Error(this.Message);

            return UnifyResponse.Success(this.Message);
        }
        #endregion
    }
    public class OperationResult<TData> : OperationResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public TData? Data { get; set; }

        #region methods
        public static OperationResult<TData> Success(TData? data = default(TData))
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Success,
                Code = "success",
                Data = data
            };
        }
        public static OperationResult<TData> Success(string code, string? msg = null, TData? data = default(TData))
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Success,
                Code = code,
                Message = msg,
                Data = data
            };
        }

        public static OperationResult<TData> Error(TData? data = default(TData))
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Failed,
                Code = "fail",
                Data = data
            };
        }

        public static OperationResult<TData> Error(string? msg = null, TData? data = default(TData))
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Failed,
                Code = "fail",
                Message = msg,
                Data = data
            };
        }

        public static OperationResult<TData> ErrorCustomCode(string code, string? msg = null, TData? data = default(TData))
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Failed,
                Code = code,
                Message = msg,
                Data = data
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new UnifyResponse<TData> ToResponse()
        {
            if (this.Status != JDA.Core.Models.Operations.OperationResultStatus.Success)
                return UnifyResponse<TData>.Error(this.Message);

            return UnifyResponse<TData>.Success(this.Message);
        }
        #endregion
    }
}
