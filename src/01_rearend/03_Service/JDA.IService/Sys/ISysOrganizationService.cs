using JDA.Core.Models.Trees;
using JDA.Core.Persistence.Entities;
using JDA.Core.Persistence.Services.Abstractions.Default;
using JDA.Entity.Entities.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JDA.IService.Sys
{
    /// <summary>
    /// 组织机构
    /// </summary>
    public interface ISysOrganizationService : IService<SysOrganization>
    {
        List<TreeNode> GetOrgTree(List<SysOrganization> models, long parentId = 0);
    }
}
