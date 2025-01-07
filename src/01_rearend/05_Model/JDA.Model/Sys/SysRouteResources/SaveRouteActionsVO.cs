using JDA.DTO.SysActionResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Model.Sys.SysRouteResources
{
    /// <summary>
    /// 保存指定菜单的功能集合模型
    /// </summary>
    public class SaveRouteActionsVO
    {
        /// <summary>
        /// 路由资源ID
        /// </summary>
        [Display(Name = "路由资源ID", Order = 1)]
        [Required(ErrorMessage = "{0}是必填项")]
        public long RouteResourceId { get; set; }
        /// <summary>
        /// 操作功能集合
        /// </summary>
        [Display(Name = "操作功能集合", Order = 1)]
        [Required(ErrorMessage = "{0}是必填项")]
        public required List<SysActionResourceDto> Actions { get; set; }
    }
}
