using JDA.Core.Attributes;
using JDA.Core.Models.ColumnMetadatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Utilities
{
    public class AttributeHelper
    {
        /// <summary>
        /// 获取给定类型的所有字段的指定的特性信息
        /// </summary>
        /// <typeparam name="TAttribute">指定的特性</typeparam>
        /// <param name="type">指定类型</param>
        /// <returns></returns>
        public static Dictionary<string, TAttribute> GetAttributeInfo<TAttribute>(Type type) where TAttribute : Attribute, new()
        {
            Dictionary<string, TAttribute> columnMetadatas = new Dictionary<string, TAttribute>();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                Type columnMetadataAttributeType = typeof(ColumnMetadataAttribute);
                PropertyInfo[] columnMetadataAttributeProperties = columnMetadataAttributeType.GetProperties();
                TAttribute columnMetadataAttribute = new TAttribute();
                CustomAttributeData columnMetadata = property.CustomAttributes.FirstOrDefault(n => n.AttributeType == columnMetadataAttributeType);
                foreach (var item in columnMetadata.NamedArguments)
                {
                    PropertyInfo propertyInfo = columnMetadataAttributeProperties.FirstOrDefault(n => n.Name == item.MemberName);
                    propertyInfo.SetValue(columnMetadataAttribute, item.TypedValue.Value);
                }
                columnMetadatas.Add(property.Name, columnMetadataAttribute);
            }

            return columnMetadatas;
        }
    }
}
