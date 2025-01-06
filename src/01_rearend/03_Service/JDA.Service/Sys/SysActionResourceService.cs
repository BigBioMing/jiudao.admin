using JDA.Core.Mappers.Abstractions;
using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Persistence.Services.Implements.Default;
using JDA.DTO.SysActionResources;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Service.Sys
{
    /// <summary>
    /// 操作资源
    /// </summary>
    public class SysActionResourceService : Service<SysActionResource>, ISysActionResourceService
    {
        public SysActionResourceService(IShapeMapper mapper, IRepository<SysActionResource> currentRepository) : base(mapper, currentRepository)
        {
        }

        /// <summary>
        /// 保存操作资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<OperationResult<SysActionResource>> SaveAsync(SysActionResourceSaveInputDto model)
        {
            if (model.Id > 0)
            {
                var entity = await _currentRepository.FirstOrDefaultAsync(n => n.Id == model.Id);
                if (entity is null) return OperationResult<SysActionResource>.Error("数据不存在");

                entity = _mapper.Map<SysActionResourceSaveInputDto, SysActionResource>(model, entity);
                return await _currentRepository.UpdateAsync(entity);
            }
            else
            {
                var entity = _mapper.Map<SysActionResource>(model);
                return await _currentRepository.InsertAsync(entity);
            }
        }
    }
}
