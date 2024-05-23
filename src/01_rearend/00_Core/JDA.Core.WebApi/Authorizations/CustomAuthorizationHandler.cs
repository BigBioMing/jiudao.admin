using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.WebApi.Authorizations
{
    public class CustomAuthorizationHandler : AuthorizationHandler<CustomRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirement requirement)
        {
            // 在这里编写自定义的授权逻辑
            bool isSuc = false;
            if (isSuc)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
