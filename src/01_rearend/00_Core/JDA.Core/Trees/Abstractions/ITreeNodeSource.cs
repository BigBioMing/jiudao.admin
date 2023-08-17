using JDA.Core.Attributes;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JDA.Core.Trees.Abstractions
{
    public interface ITreeNodeSource
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }
        public int Sort { get; set; }
    }
}
