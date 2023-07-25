using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Utilities
{
    public static class TypeHelper
    {
        /// <summary>
        /// 判断指定的类型 <paramref name="type"/> 是否是指定泛型类型的子类型，或实现了指定泛型接口。
        /// </summary>
        /// <param name="type">需要测试的类型。</param>
        /// <param name="generic">泛型接口类型，传入 typeof(IXxx&lt;&gt;)</param>
        /// <returns>如果是泛型接口的子类型，则返回 true，否则返回 false。</returns>
        public static bool HasImplementedRawGeneric([NotNull] Type type, [NotNull] Type generic)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (generic == null) throw new ArgumentNullException(nameof(generic));

            // 测试接口。
            var isTheRawGenericType = type.GetInterfaces().Any(IsTheRawGenericType);
            if (isTheRawGenericType) return true;

            // 测试类型。
            while (type != null && type != typeof(object))
            {
                isTheRawGenericType = IsTheRawGenericType(type);
                if (isTheRawGenericType) return true;
                type = type.BaseType;
            }

            // 没有找到任何匹配的接口或类型。
            return false;

            // 测试某个类型是否是指定的原始接口。
            bool IsTheRawGenericType(Type test)
                => generic == (test.IsGenericType ? test.GetGenericTypeDefinition() : test);
        }

        /// <summary>
        /// 查找指定类型继承的某个基类
        /// </summary>
        /// <param name="currentType">当前类型</param>
        /// <param name="baseType">要查找的基类</param>
        /// <returns></returns>
        public static Type? FindBaseType(Type currentType, Type baseType)
        {
            if (currentType is null) throw new ArgumentNullException();
            if (currentType.BaseType is null) return null;
            if (currentType.BaseType.IsGenericType)
            {
                if (currentType.BaseType.GetGenericTypeDefinition() == baseType) return currentType.BaseType;
            }
            else
            {
                if (currentType.BaseType == baseType) return currentType.BaseType;
            }
            return FindBaseType(currentType.BaseType, baseType);
        }
    }
}
