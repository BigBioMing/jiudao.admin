﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Views.ViewModels.Base
{
    public class BaseSugerEntityViewModel<TKey> : BaseViewModel
    {
    }
    public class BaseSugerEntityViewModel : BaseSugerEntityViewModel<long>
    {
    }
}
