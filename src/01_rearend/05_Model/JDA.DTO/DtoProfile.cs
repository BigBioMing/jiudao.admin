using JDA.Core.Mappers.Abstractions;
using JDA.DTO.SysActionResources;
using JDA.DTO.SysRoles;
using JDA.DTO.SysUsers;
using JDA.Entity.Entities.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.DTO
{
    public class DtoProfile : ShapeWishProfile
    {
        public DtoProfile()
        {
            CreateMap<SysRouteResource, UserMenuTreeDto>().ReverseMap();
            CreateMap<SysRouteResource, MenuTreeDto>().ReverseMap();
            CreateMap<SysActionResource, ActionTreeDto>().ReverseMap();
            CreateMap<SysActionResource, SysActionResourceDto>().ReverseMap(); 
            CreateMap<SysActionResource, SysActionResourceDto>().ReverseMap(); 
        }
    }
}
