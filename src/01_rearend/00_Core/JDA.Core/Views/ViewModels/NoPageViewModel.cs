using JDA.Core.Models.Tables;
using JDA.Core.Views.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Views.ViewModels
{
    public class NoPageViewModel : BaseViewModel
    {
        /// <summary>
        /// 分页参数
        /// </summary>
        public dynamic? Params { get; set; }
    }
}
