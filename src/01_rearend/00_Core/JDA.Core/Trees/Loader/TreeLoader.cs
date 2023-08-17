using JDA.Core.Trees.Abstractions;
using JDA.Core.Trees.Implements;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core.DAG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Trees.Loader
{
    public class TreeLoader
    {
        /// <summary>
        /// 获取树
        /// </summary>
        /// <typeparam name="TTreeNodeSource"></typeparam>
        /// <typeparam name="TTreeNode"></typeparam>
        /// <param name="sources">数据源</param>
        /// <param name="parentId">指定从哪一级开始获取</param>
        /// <returns></returns>
        public static List<TTreeNode> GetTrees<TTreeNodeSource, TTreeNode>(List<TTreeNodeSource> sources, long parentId = 0) where TTreeNodeSource : ITreeNodeSource where TTreeNode : ITreeNode<TTreeNode>, new()
        {
            List<TTreeNode> list = new List<TTreeNode>();
            var items = sources.Where(n => n.ParentId == parentId).ToList();

            foreach (var item in items)
            {
                TTreeNode node = new TTreeNode()
                {
                    Sort = item.Sort,
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Code = item.Code,
                    Name = item.Name
                };
                node.Childrens = TreeLoader.GetTrees<TTreeNodeSource, TTreeNode>(sources, item.Id);
                list.Add(node);
            }

            return list;
        }
        /// <summary>
        /// 获取树
        /// </summary>
        /// <typeparam name="TTreeNodeSource"></typeparam>
        /// <param name="sources">数据源</param>
        /// <param name="parentId">指定从哪一级开始获取</param>
        /// <returns></returns>
        public static List<TreeNode> GetTrees<TTreeNodeSource>(List<TTreeNodeSource> sources, long parentId = 0) where TTreeNodeSource : ITreeNodeSource
        {
            return TreeLoader.GetTrees<TTreeNodeSource, TreeNode>(sources, parentId);
        }
    }
}
