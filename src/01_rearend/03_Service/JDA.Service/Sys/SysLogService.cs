using JDA.Core.Mappers.Abstractions;
using JDA.Core.Models.Operations;
using JDA.Core.Persistence.Repositories.Abstractions.Default;
using JDA.Core.Persistence.Services.Implements.Default;
using JDA.Core.WebApi.HttpLoggings;
using JDA.Entity.Entities.Sys;
using JDA.IService.Sys;
using JDA.Model.Sys.SysLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Service.Sys
{
    /// <summary>
    /// HTTP请求响应日志
    /// </summary>
    public partial class SysLogService : Service<SysLog>, ISysLogService, IHttpLoggingStorage
    {
        public SysLogService(
            IShapeMapper mapper
            , IRepository<SysLog> currentRepository
            ) : base(mapper, currentRepository)
        {
        }

        public async Task<OperationResult<SysLog>> SaveAsync(SysLog model)
        {
            return await this._currentRepository.InsertAsync(model);
        }

        public async Task<OperationResult> SaveAsync(HttpLoggingInformation logInfo)
        {
            SysLog log = _mapper.Map<SysLog>(logInfo);
            return await this._currentRepository.InsertAsync(log);
        }

        public async Task<OperationResult> SaveAsync(List<HttpLoggingInformation> list)
        {
            List<SysLog> lst = new List<SysLog>();
            foreach (var item in list)
            {
                SysLog log = _mapper.Map<SysLog>(item);
                lst.Add(log);
            }
            return await this._currentRepository.InsertAsync(lst);
        }
    }
}
