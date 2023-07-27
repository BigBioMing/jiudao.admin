using JDA.Core.Persistence.Entities;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Repositories.Abstractions
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IRepository<TEntity> : IBaseRepository<TEntity> where TEntity : SuperEntity
    {
    }
}
