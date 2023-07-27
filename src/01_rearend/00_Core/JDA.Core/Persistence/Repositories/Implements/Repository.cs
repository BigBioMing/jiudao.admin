﻿using JDA.Core.Persistence.Contexts;
using JDA.Core.Persistence.Entities;
using JDA.Core.Persistence.Repositories.Abstractions;

namespace JDA.Core.Persistence.Repositories.Implements
{
    /// <summary>
    /// 仓储类
    /// </summary>
    public class Repository<TEntity> : BaseRepository<TEntity>, IRepository<TEntity> where TEntity : SuperEntity
    {
        public Repository(JDABaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}