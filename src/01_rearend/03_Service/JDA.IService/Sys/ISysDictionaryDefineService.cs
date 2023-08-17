using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Entity.Entities.Sys;
using JDA.Model.Sys.SysDictionaryDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JDA.IService.Sys
{
    public interface ISysDictionaryDefineService : IService<SysDictionaryDefine>
    {
        Task<List<SysDictionaryDefineTreeVO>> GetDictionaryTree(Expression<Func<SysDictionaryDefine, bool>>? predicate = null);
    }
}
