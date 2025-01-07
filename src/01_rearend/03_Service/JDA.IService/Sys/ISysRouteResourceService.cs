using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.DTO.SysActionResources;
using JDA.DTO.SysRoles;
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
        /// <summary>
        /// 获取菜单和按钮
        /// </summary>
        /// <returns></returns>
        Task<List<MenuTreeDto>> GetMenuAndActionsAsync();
        /// <summary>
        /// 获取指定菜单拥有的按钮集合
        /// </summary>
        /// <param name="routeResourceId"></param>
        /// <returns></returns>
        Task<List<SysActionResourceDto>> GetActionsAsync(long routeResourceId);
        /// <summary>
        /// 保存指定菜单下的按钮集合
        /// </summary>
        /// <param name="routeResourceId">路由资源Id</param>
        /// <param name="actions">该菜单要保存的按钮集合</param>
        /// <returns></returns>
        Task SaveActionsAsync(long routeResourceId, List<SysActionResourceDto> actions);
    }
}
