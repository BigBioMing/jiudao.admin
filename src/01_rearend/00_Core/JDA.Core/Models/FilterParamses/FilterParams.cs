using JDA.Core.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Models.FilterParamses
{
    public class FilterParams
    {
        public PageInParams Page { get; set; }
        public dynamic Params { get; set; }
    }
}
