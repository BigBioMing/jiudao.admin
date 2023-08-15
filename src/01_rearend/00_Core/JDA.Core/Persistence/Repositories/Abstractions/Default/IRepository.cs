using JDA.Core.Persistence.Contexts.Default;
using JDA.Core.Persistence.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Repositories.Abstractions.Default
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IRepository<TEntity> : IBaseRepository<TEntity, JDADbContext> where TEntity : class, ISuperEntity
    {
    }
}
