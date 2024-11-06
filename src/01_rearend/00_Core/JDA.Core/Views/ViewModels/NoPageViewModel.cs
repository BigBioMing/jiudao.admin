using JDA.Core.ModelBinds;
using JDA.Core.Models.Tables;
using JDA.Core.Views.ViewModels.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        //[ModelBinder(BinderType = typeof(CustomModelBinder))]
        public dynamic? Params { get; set; }
    }
}
