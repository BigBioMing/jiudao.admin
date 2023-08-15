using JDA.Core.Models.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Entities
{
    /// <summary>
    /// 树形表接口
    /// </summary>
    public interface ITreeNodeSuperEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }
        public int Sort { get; set; }
    }
}
