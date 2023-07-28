using JDA.Api.Core.Users.Implements;
using JDA.Core.DI.Abstractions;
using JDA.Core.System.Abstractions;
using JDA.Core.Users.Abstractions;
using JDA.Core.Users.Implements;

namespace JDA.Api.Core.Users.DI
{
    /// <summary>
    /// 注册当前执行人（HTTP）
    /// </summary>
    public class HttpUsersDependencyInjectionRegistrar : IDependencyInjectionRegistrar
    {
        public int Order => 2;

        public string Key => "HttpUsers";

        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICurrentRunningContext>(serviceProvider =>
            {
                IHttpContextAccessor _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                IMachineInformation _machineInformation = serviceProvider.GetRequiredService<IMachineInformation>();
                DefaultCurrentRunningContext context = new DefaultCurrentRunningContext(_machineInformation);
                return new HttpUsersCurrentRunningContext(_httpContextAccessor, context);
            });
        }
    }
}
