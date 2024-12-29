using JDA.DTO.SysActionResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.DTO.SysUsers
{
    /// <summary>
    /// 用户拥有的菜单及按钮权限dto
    /// </summary>
    public class UserMenuAndActionDto
    {
        /// <summary>
        /// 菜单树节点集合
        /// </summary>
        public List<MenuTreeDto> MenuTreeNodes { get; set; }
        /// <summary>
        /// 操作（按钮）集合
        /// </summary>
        public List<SysActionResourceDto> Actions { get; set; }
    }
}
