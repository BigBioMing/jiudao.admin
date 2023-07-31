using JDA.Core.Views.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Views.ViewModels
{
    /// <summary>
    /// 启用/禁用模型（字典）
    /// </summary>
    public class DicEnableViewModel : BaseViewModel
    {
        public long Id { get; set; }
        public bool SetEnableValue { get; set; }
    }
}
