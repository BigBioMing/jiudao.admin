using JDA.Core.Mappers.Abstractions;
using JDA.Core.Persistence.Contexts.Default;
using JDA.Core.Persistence.Entities.Abstractions;
using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Persistence.Services.Abstractions.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Services.Implements.Default
{
    /// <summary>
    /// Service类
    /// </summary>
    public class Service<TEntity> : BaseService<TEntity, JDADbContext>, IService<TEntity> where TEntity : class, ISuperEntity
    {
        public Service(IShapeMapper mapper, IRepository<TEntity> currentRepository) : base(mapper, currentRepository)
        {
        }
    }
}
