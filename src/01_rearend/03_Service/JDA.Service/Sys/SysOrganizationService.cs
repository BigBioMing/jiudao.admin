using JDA.Core.Mappers.Abstractions;
using JDA.Core.Models.Trees;
using JDA.Core.Persistence.Entities;
using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Persistence.Services.Implements.Default;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Service.Sys
{
    /// <summary>
    /// 组织机构
    /// </summary>
    public class SysOrganizationService : Service<SysOrganization>, ISysOrganizationService
    {
        public SysOrganizationService(IShapeMapper mapper, IRepository<SysOrganization> currentRepository) : base(mapper, currentRepository)
        {
        }
        public virtual List<TreeNode> GetOrgTree(List<SysOrganization> models, long parentId = 0)
        {
            List<TreeNode> list = new List<TreeNode>();
            var items = models.Where(n => n.ParentId == parentId).ToList();

            foreach (var item in items)
            {
                TreeNode node = new TreeNode()
                {
                    Sort = item.Sort,
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Name = item.Name
                };
                node.Childrens = GetOrgTree(models, item.Id);
                list.Add(node);
            }

            return list;
        }
    }
}
