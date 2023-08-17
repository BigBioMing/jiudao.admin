using JDA.Core.Mappers.Abstractions;
using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Persistence.Services.Implements.Default;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using JDA.Model.Sys.SysDictionaryDatas;
using JDA.Model.Sys.SysDictionaryDefines;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Service.Sys
{
    public class SysDictionaryDefineService : Service<SysDictionaryDefine>, ISysDictionaryDefineService
    {
        protected readonly IRepository<SysDictionaryData> _sysDictionaryDataRepository;
        public SysDictionaryDefineService(IShapeMapper mapper, IRepository<SysDictionaryDefine> currentRepository, IRepository<SysDictionaryData> sysDictionaryDataRepository) : base(mapper, currentRepository)
        {
            this._sysDictionaryDataRepository = sysDictionaryDataRepository;
        }

        public virtual async Task<List<SysDictionaryDefineTreeVO>> GetDictionaryTree(Expression<Func<SysDictionaryDefine, bool>>? predicate = null)
        {
            var defines = await this.GetEntitiesAsync(predicate);
            var defineTrees = this._mapper.Map<List<SysDictionaryDefineTreeVO>>(defines);
            if (defines.Any())
            {
                List<long> defineIds = defines.Select(n => n.Id).ToList();
                var dicItems = await this._sysDictionaryDataRepository.GetEntitiesAsync(n => defineIds.Contains(n.Id));
                foreach (var defineTree in defineTrees)
                {
                    var items = dicItems.Where(n => n.DictionaryDefineId == defineTree.Id).ToList();
                    defineTree.Childrens = this._mapper.Map<List<SysDictionaryDataTreeNodeVO>>(items);
                }
            }

            return defineTrees;
        }
    }
}
