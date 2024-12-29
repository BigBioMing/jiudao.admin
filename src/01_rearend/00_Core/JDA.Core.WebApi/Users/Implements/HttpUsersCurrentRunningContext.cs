using JDA.Core.Users.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace JDA.Core.WebApi.Users.Implements
{
    public class HttpUsersCurrentRunningContext : ICurrentRunningContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentRunningContext _currentRunningContext;

        public HttpUsersCurrentRunningContext(IHttpContextAccessor httpContextAccessor, ICurrentRunningContext currentRunningContext)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._currentRunningContext = currentRunningContext;
        }

        /// <summary>
        /// 当前操作人ID（当前登陆人）
        /// </summary>
        public long UserId
        {
            get
            {
                string? identifier = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                return long.TryParse(identifier, out long userId) ? userId : this._currentRunningContext.UserId;
            }
        }

        /// <summary>
        /// 当前操作人名称（当前登陆人）
        /// </summary>
        public string UserName
        {
            get
            {
                string? userName = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
                if (string.IsNullOrEmpty(userName)) return this._currentRunningContext.UserName;
                return userName;
            }
        }
    }
}
