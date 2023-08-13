using AutoMapper;
using JDA.Core.Mappers.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Mappers.Implements
{
    /// <summary>
    /// 对象映射转换
    /// </summary>
    public class ShapeMapper : IShapeMapper
    {
        private readonly IMapper _mapper;
        public ShapeMapper(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public TDestination Map<TDestination>(object source) => this._mapper.Map(source, default(TDestination));

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return this._mapper.Map<TSource, TDestination>(source);
        }
    }
}
