using JDA.Core.Persistence.Contexts;
using JDA.Core.Persistence.Entities;
using JDA.IRepository.Base;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Repository.Base
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
