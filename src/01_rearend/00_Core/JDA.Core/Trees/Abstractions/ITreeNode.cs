using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Trees.Abstractions
{
    public interface ITreeNode
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }
        public int Sort { get; set; }
    }

    public interface ITreeNode<TTreeNode> : ITreeNode where TTreeNode : ITreeNode<TTreeNode>
    {
        public List<TTreeNode> Childrens { get; set; }
    }
}
