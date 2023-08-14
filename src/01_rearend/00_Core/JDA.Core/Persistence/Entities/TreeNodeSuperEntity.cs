using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Entities
{
    /// <summary>
    /// 树形表超类
    /// </summary>
    public class TreeNodeSuperEntity : EnableSuperEntity, ITreeNodeSuperEntity
    {
        public string Name { get; set; }
        public long ParentId { get; set; }
        public int Sort { get; set; }
    }
}
