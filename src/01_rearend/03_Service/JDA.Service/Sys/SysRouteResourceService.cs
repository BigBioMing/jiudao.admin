using JDA.Core.Mappers.Abstractions;
using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Persistence.Services.Implements.Default;
using JDA.DTO.SysActionResources;
using JDA.DTO.SysRoles;
using JDA.DTO.SysRouteResources;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Service.Sys
{
    /// <summary>
    /// 路由资源
    /// </summary>
    public class SysRouteResourceService : Service<SysRouteResource>, ISysRouteResourceService
    {
        public SysRouteResourceService(IShapeMapper mapper, IRepository<SysRouteResource> currentRepository) : base(mapper, currentRepository)
        {
        }

        #region 获取路由资源树
        /// <summary>
        /// 获取路由资源树
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<RouteTreeDto>> GetRouteTreeAsync()
        {
            var allMenus = await _currentRepository.QueryableNoTracking.OrderBy(n => n.Sort).ToListAsync();
            //获取菜单树
            var topMenuTreeNodes = LoopMenus(allMenus, null);

            return topMenuTreeNodes;
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="allMenus">所有菜单</param>
        /// <param name="parentMenu">父级菜单（为null时表示从1级菜单开始获取</param>
        /// <returns></returns>
        private List<RouteTreeDto> LoopMenus(List<SysRouteResource> allMenus, SysRouteResource? parentMenu)
        {
            List<RouteTreeDto> childMenuTreeNodes = new List<RouteTreeDto>();
            //根据ParentId获取下级菜单，然后组装数据
            long parentId = parentMenu?.Id ?? 0;
            var currentMenus = allMenus.Where(n => n.ParentId == parentId).OrderBy(n => n.Sort).ToList();
            foreach (var currentMenu in currentMenus)
            {
                //组装子级节点数据
                RouteTreeDto childMenuTreeNode = this._mapper.Map<RouteTreeDto>(currentMenu);
                childMenuTreeNodes.Add(childMenuTreeNode);

                //获取下下级菜单
                var grandsonMenuTreeNodes = LoopMenus(allMenus, currentMenu);
                childMenuTreeNode.Childrens = grandsonMenuTreeNodes;
                if (childMenuTreeNode.Childrens?.Count == 0) childMenuTreeNode.Childrens = null;
            }

            return childMenuTreeNodes;
        }
        #endregion
    }
}
