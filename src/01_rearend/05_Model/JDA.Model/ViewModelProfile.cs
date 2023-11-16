using AutoMapper;
using JDA.Core.Mappers.Abstractions;
using JDA.Core.WebApi.HttpLoggings;
using JDA.Entity.Entities.Sys;
using JDA.Model.Sys.SysDictionaryDatas;
using JDA.Model.Sys.SysDictionaryDefines;
using JDA.Model.Sys.SysLogs;
using JDA.Model.Sys.SysRoles;
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
            CreateMap<SysUser, SysUserSaveVO>().ReverseMap();
            CreateMap<SysDictionaryDefine, SysDictionaryDefineTreeVO>().ReverseMap();
            CreateMap<SysDictionaryData, SysDictionaryDataTreeNodeVO>().ReverseMap();
            CreateMap<SysLog, SysLogSaveVO>().ReverseMap();
            CreateMap<SysLog, HttpLoggingInformation>().ReverseMap();
        }
    }
}
