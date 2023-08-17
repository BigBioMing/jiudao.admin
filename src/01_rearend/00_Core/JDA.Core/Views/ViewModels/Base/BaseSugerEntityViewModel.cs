﻿using JDA.Core.Attributes;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JDA.Core.Views.ViewModels.Base
{
    public class BaseSugerEntityViewModel<TKey> : BaseViewModel
    {
    }
    public class BaseSugerEntityViewModel : BaseSugerEntityViewModel<long>
    {
    }
}
