using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.DTO.SysRouteResources;
using JDA.Entity.Entities.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.IService.Sys
{
    /// <summary>
    /// 路由资源
    /// </summary>
    public interface ISysRouteResourceService : IService<SysRouteResource>
    {
        /// <summary>
        /// 保存路由资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<OperationResult<SysRouteResource>> SaveAsync(SysRouteResourceSaveInputDto model);
        /// <summary>
        /// 获取路由资源树
        /// </summary>
        /// <returns></returns>
        Task<List<RouteTreeDto>> GetRouteTreeAsync();
    }
}
