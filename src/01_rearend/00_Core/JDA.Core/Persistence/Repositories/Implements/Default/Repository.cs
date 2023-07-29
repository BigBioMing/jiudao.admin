using JDA.Core.Persistence.Contexts.Default;
using JDA.Core.Persistence.Entities;
using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Users.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Repositories.Implements.Default
{
    /// <summary>
    /// 仓储类
    /// </summary>
    public class Repository<TEntity> : BaseRepository<TEntity, JDADbContext>, IRepository<TEntity> where TEntity : SuperEntity
    {
        public Repository(JDADbContext dbContext, ICurrentRunningContext currentRunningContext) : base(dbContext, currentRunningContext)
        {
        }
    }
}
