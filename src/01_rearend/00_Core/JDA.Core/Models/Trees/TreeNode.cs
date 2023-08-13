using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Models.Trees
{
    /// <summary>
    /// 树状对象
    /// </summary>
    public class TreeNode
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }
        public int Sort { get; set; }
        public List<TreeNode> Childrens { get; set; }
    }
}
