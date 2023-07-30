using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Attributes
{
    /// <summary>
    /// 列元数据信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ColumnMetadataAttribute : Attribute
    {
        public ColumnMetadataAttribute()
        {
            this.IsDic = false;
            this.Hidden = false;
        }
        public string Name { get; set; }
        public int Order { get; set; }
        public bool Hidden { get; set; }
        public bool IsDic { get; set; }
        public string DicKey { get; set; }
        public string Format { get; set; }
        public bool IsEncry { get; set; }
        public string EncryType { get; set; }
    }
}
