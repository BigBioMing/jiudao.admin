using JDA.Core.Models.Tables;
using JDA.Core.Views.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Views.ViewModels
{
    public class PageViewModel : NoPageViewModel
    {
        /// <summary>
        /// 分页大小
        /// </summary>
        public PageInParams Page { get; set; }
    }
}
