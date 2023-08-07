using JDA.Core.Attributes;
using JDA.Core.Views.ViewModels.Base;
using Microsoft.EntityFrameworkCore.Metadata;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JDA.Model.Sys.SysRoles
{
    /// <summary>
    /// 角色授权模型
    /// </summary>
    public class SysRoleEmpowerVO : BaseSugerEntityViewModel
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Display(Name = "角色ID", Order = 1)]
        [Description("角色ID")]
        [ColumnMetadata(Name = "路由资源ID", Order = 1)]
        public long RoleId { get; set; }
        /// <summary>
        /// 需要给指定角色添加的路由资源ID集合
        /// </summary>
        [Display(Name = "需要给指定角色添加的路由资源ID集合", Order = 2)]
        [ColumnMetadata(Name = "需要给指定角色添加的路由资源ID集合", Order = 2)]
        public List<long> RoleRouteResourceIds { get; set; }
    }
}
