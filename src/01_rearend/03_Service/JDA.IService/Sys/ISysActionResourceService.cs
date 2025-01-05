using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.DTO.SysActionResources;
using JDA.Entity.Entities.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.IService.Sys
{
    /// <summary>
    /// 操作资源
    /// </summary>
    public interface ISysActionResourceService : IService<SysActionResource>
    {
        /// <summary>
        /// 保存操作资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<OperationResult<SysActionResource>> SaveAsync(SysActionResourceSaveInputDto model);
    }
}
