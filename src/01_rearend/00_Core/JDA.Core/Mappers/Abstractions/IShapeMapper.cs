using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Mappers.Abstractions
{
    /// <summary>
    /// 对象映射转换
    /// </summary>
    public interface IShapeMapper
    {
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
        TDestination Map<TDestination>(object source);
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
