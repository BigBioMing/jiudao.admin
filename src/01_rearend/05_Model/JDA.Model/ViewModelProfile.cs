using AutoMapper;
using JDA.Core.Mappers.Abstractions;
using JDA.Entity.Entities.Sys;
using JDA.Model.Sys.SysUsers;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JDA.Model
{
    public class ViewModelProfile : ShapeWishProfile
    {
        public ViewModelProfile()
        {
            CreateMap<SysUser, SysUserSaveViewModel>().ReverseMap();
        }
    }
}
