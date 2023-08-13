using JDA.Core.Mappers.Abstractions;
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
    }
}
