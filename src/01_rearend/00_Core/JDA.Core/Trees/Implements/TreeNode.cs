using JDA.Core.Trees.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Trees.Implements
{
    public class TreeNode : ITreeNode<TreeNode>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }
        public int Sort { get; set; }
        public List<TreeNode> Childrens { get; set; }
    }
}
