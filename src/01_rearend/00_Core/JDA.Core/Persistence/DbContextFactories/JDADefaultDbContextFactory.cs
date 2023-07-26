using JDA.Core.Persistence.Contexts;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.DbContextFactories
{
    /// <summary>
    /// 数据库上下文工厂
    /// 由于在上下文池中的上下文对象只会初始化一次，由此会导致随着请求改变的状态（如TenantId）无法更新的问题，可以在工厂里获取上下文对象后重新赋值
    /// </summary>
    public class JDADefaultDbContextFactory<TDbContext> : IDbContextFactory<TDbContext> where TDbContext : JDABaseDbContext
    {
        private const int DefaultTenantId = -1;

        private readonly IDbContextFactory<TDbContext> _pooledFactory;
        private readonly int _tenantId;

        public JDADefaultDbContextFactory(
            IDbContextFactory<TDbContext> pooledFactory,
            ITenant tenant)
        {
            _pooledFactory = pooledFactory;
            _tenantId = int.TryParse(tenant?.TenantId, out _tenantId) ? _tenantId : DefaultTenantId;
        }

        public virtual TDbContext CreateDbContext()
        {
            var context = _pooledFactory.CreateDbContext();
            context.TenantId = _tenantId;
            return context;
        }
    }
}
