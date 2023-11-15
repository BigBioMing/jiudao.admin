using JDA.Core.Exceptions;
using JDA.Core.Formats.WebApi;
using JDA.Core.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.Filters
{
    public class ApiActionFilter : IActionFilter, IAsyncActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            LoggerHelper.Default.Info("OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            LoggerHelper.Default.Info("OnActionExecuted");
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            LoggerHelper.Default.Info("OnActionExecutionAsync before");
            ActionExecutedContext actionExecutedContext = await next();
            if (actionExecutedContext != null && actionExecutedContext.Exception is null)
            {
                if (actionExecutedContext.Result is JsonResult jsonResult)
                {
                    if (!(jsonResult.Value is UnifyResponse))
                    {
                        actionExecutedContext.Result = new JsonResult(new UnifyResponse() { Message = "success", Code = UnifyResponse.SuccessCode, Data = jsonResult.Value });
                    }
                }
                else if (actionExecutedContext.Result is ObjectResult objectResult)
                {
                    if (!(objectResult.Value is UnifyResponse))
                    {
                        actionExecutedContext.Result = new JsonResult(new UnifyResponse() { Message = "success", Code = UnifyResponse.SuccessCode, Data = objectResult.Value });
                    }
                }
                else
                {
                    if (!(actionExecutedContext.Result is UnifyResponse))
                    {
                        if (actionExecutedContext.Result is not EmptyResult)
                            actionExecutedContext.Result = new JsonResult(new UnifyResponse() { Message = "success", Code = UnifyResponse.SuccessCode, Data = actionExecutedContext.Result });
                        else
                            actionExecutedContext.Result = new JsonResult(new UnifyResponse() { Message = "success", Code = UnifyResponse.SuccessCode, Data = null });
                    }
                }
            }

            LoggerHelper.Default.Info("OnActionExecutionAsync after");
        }
    }
}
