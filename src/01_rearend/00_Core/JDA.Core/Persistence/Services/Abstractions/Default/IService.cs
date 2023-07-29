using JDA.Core.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Services.Abstractions.Default
{
    /// <summary>
    /// Service接口
    /// </summary>
    public interface IService<TEntity> : IBaseService<TEntity> where TEntity : SuperEntity
    {
    }
}
