using JDA.Core.Persistence.Entities;
using JDA.Core.Persistence.Repositories.Abstractions;
using JDA.Core.Persistence.Repositories.Implements;
using JDA.Core.Persistence.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Services.Implements
{
    /// <summary>
    /// Service类
    /// </summary>
    public class Service<TEntity> : BaseService<TEntity>, IService<TEntity> where TEntity : SuperEntity
    {
        public Service(IRepository<TEntity> currentRepository) : base(currentRepository)
        {
        }
    }
}
