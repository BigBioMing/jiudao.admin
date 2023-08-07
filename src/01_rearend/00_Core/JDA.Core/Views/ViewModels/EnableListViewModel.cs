using JDA.Core.Views.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Views.ViewModels
{
    /// <summary>
    /// 启用/禁用模型
    /// </summary>
    public class EnableListViewModel : BaseViewModel
    {
        /// <summary>
        /// 要启用/禁用的数据的ID的集合
        /// </summary>
        public List<long> Ids { get; set; }
        /// <summary>
        /// 要设置的值（启用或禁用）
        /// </summary>
        public long SetEnableValue { get; set; }
    }
}
