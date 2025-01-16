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
        protected readonly IRepository<SysActionResource> _sysActionResourceRepository;
        protected readonly IRepository<SysRouteResource> _sysRouteResourceRepository;
        public SysRouteResourceService(IShapeMapper mapper, IRepository<SysRouteResource> currentRepository, IRepository<SysActionResource> sysActionResourceRepository, IRepository<SysRouteResource> sysRouteResourceRepository) : base(mapper, currentRepository)
        {
            _sysActionResourceRepository = sysActionResourceRepository;
            _sysRouteResourceRepository = sysRouteResourceRepository;
        }

        /// <summary>
        /// 保存路由资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<OperationResult<SysRouteResource>> SaveAsync(SysRouteResourceSaveInputDto model)
        {
            if (model.Id > 0)
            {
                var entity = await _currentRepository.FirstOrDefaultAsync(n => n.Id == model.Id);
                if (entity is null) return OperationResult<SysRouteResource>.Error("数据不存在");

                entity = _mapper.Map<SysRouteResourceSaveInputDto, SysRouteResource>(model, entity);
                return await _currentRepository.UpdateAsync(entity);
            }
            else
            {
                var entity = _mapper.Map<SysRouteResource>(model);
                return await _currentRepository.InsertAsync(entity);
            }
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
            long? parentId = parentMenu?.Id ?? null;
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


        #region 获取菜单和按钮
        /// <summary>
        /// 获取菜单和按钮
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<MenuTreeDto>> GetMenuAndActionsAsync()
        {
            var allMenus = await _sysRouteResourceRepository.QueryableNoTracking.OrderBy(n => n.Sort).ToListAsync();
            var allActions = await _sysActionResourceRepository.QueryableNoTracking.OrderBy(n => n.Sort).ToListAsync();

            //获取菜单树
            var topMenuTreeNodes = LoopMenus(allMenus, allActions, null);

            return topMenuTreeNodes;
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="allMenus">所有菜单</param>
        /// <param name="allActions">所有按钮</param>
        /// <param name="parentMenu">父级菜单（为null时表示从1级菜单开始获取</param>
        /// <returns></returns>
        private List<MenuTreeDto> LoopMenus(List<SysRouteResource> allMenus, List<SysActionResource> allActions, SysRouteResource? parentMenu)
        {
            List<MenuTreeDto> childMenuTreeNodes = new List<MenuTreeDto>();
            //根据ParentId获取下级菜单，然后组装数据
            long? parentId = parentMenu?.Id ?? null;
            var currentMenus = allMenus.Where(n => n.ParentId == parentId).OrderBy(n => n.Sort).ToList();
            foreach (var currentMenu in currentMenus)
            {
                //组装子级节点数据
                MenuTreeDto childMenuTreeNode = this._mapper.Map<MenuTreeDto>(currentMenu);
                childMenuTreeNodes.Add(childMenuTreeNode);
                //获取该菜单下的按钮
                var currrentMenusActions = allActions.Where(n => n.RouteResourceId == currentMenu.Id).ToList();
                childMenuTreeNode.Actions = new List<ActionTreeDto>();

                foreach (var currrentMenusAction in currrentMenusActions)
                {
                    var currrentMenusActionTreeNode = _mapper.Map<ActionTreeDto>(currrentMenusAction);
                    childMenuTreeNode.Actions.Add(currrentMenusActionTreeNode);
                }

                //获取下下级菜单
                var grandsonMenuTreeNodes = LoopMenus(allMenus, allActions, currentMenu);
                childMenuTreeNode.Childrens = grandsonMenuTreeNodes;
                if (childMenuTreeNode.Childrens?.Count == 0) childMenuTreeNode.Childrens = null;
            }

            return childMenuTreeNodes;
        }
        #endregion


        /// <summary>
        /// 获取指定菜单拥有的按钮集合
        /// </summary>
        /// <param name="routeResourceId"></param>
        /// <returns></returns>
        public async Task<List<SysActionResourceDto>> GetActionsAsync(long routeResourceId)
        {
            var actions = await _sysActionResourceRepository.GetEntitiesAsync(n => n.RouteResourceId == routeResourceId);
            var list = _mapper.Map<List<SysActionResourceDto>>(actions);
            return list;
        }
        /// <summary>
        /// 保存指定菜单下的按钮集合
        /// </summary>
        /// <param name="routeResourceId">路由资源Id</param>
        /// <param name="actions">该菜单要保存的按钮集合</param>
        /// <returns></returns>
        public async Task SaveActionsAsync(long routeResourceId, List<SysActionResourceDto> actions)
        {
            var newActionCodes = actions.Select(n => n.Code).ToList();
            //现有的菜单集合
            var oldActions = await _sysActionResourceRepository.GetEntitiesAsync(n => n.RouteResourceId == routeResourceId);
            //筛选出要删除的，新增的，和修改的
            //待删除的
            var deleteActions = oldActions.Where(n => !newActionCodes.Contains(n.Code)).ToList();
            //待新增的
            var addActions = actions.Where(n => oldActions.Count(c => c.Code == n.Code) == 0).Select(n => new SysActionResource() { Code = n.Code, Name = n.Name, Sort = n.Sort ?? 0, RouteResourceId = routeResourceId }).ToList();
            //待修改的
            List<SysActionResource> updateActions = new List<SysActionResource>();
            foreach (var upAction in oldActions)
            {
                var newAction = actions.FirstOrDefault(n => n.Code == upAction.Code);
                if (newAction is null) continue;

                if (upAction.Name != newAction.Name || upAction.Code != newAction.Code || upAction.Sort != newAction.Sort)
                {
                    upAction.Name = newAction.Name;
                    upAction.Code = newAction.Code;
                    upAction.Sort = newAction.Sort ?? 0;
                    updateActions.Add(upAction);
                }
            }


            if (deleteActions.Count > 0) await _sysActionResourceRepository.DeleteAsync(deleteActions);
            if (addActions.Count > 0) await _sysActionResourceRepository.InsertAsync(addActions);
            if (updateActions.Count > 0) await _sysActionResourceRepository.UpdateAsync(updateActions);
        }
    }
}
