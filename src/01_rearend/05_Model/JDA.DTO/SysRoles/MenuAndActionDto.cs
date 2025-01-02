using JDA.DTO.SysActionResources;
using JDA.DTO.SysUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.DTO.SysRoles
{
    /// <summary>
    /// 菜单及按钮权限dto
    /// </summary>
    public class MenuAndActionDto
    {
        /// <summary>
        /// 菜单树节点集合
        /// </summary>
        public List<MenuTreeDto> MenuTreeNodes { get; set; }
        /// <summary>
        /// 选中的菜单Id集合
        /// </summary>
        public List<long> SelectMenuIds { get; set; }
    }
}
