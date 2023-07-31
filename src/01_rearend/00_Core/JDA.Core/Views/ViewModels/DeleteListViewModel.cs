using JDA.Core.Views.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Views.ViewModels
{
    /// <summary>
    /// 删除模型
    /// </summary>
    public class DeleteListViewModel : BaseViewModel
    {
        public List<long> Ids { get; set; }
    }
}
