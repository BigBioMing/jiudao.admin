using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Utilities
{
    public class DataTableHelper
    {
        /// <summary>
        /// DataTable转List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(DataTable dt) where T : new()
        {
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] properties = t.GetType().GetProperties();
                foreach (var item in properties)
                {
                    if (dt.Columns.Contains(item.Name))
                    {
                        object value = dr[item.Name];
                        item.SetValue(t, value, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }

        /// <summary>
        /// List转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> list)
        {
            DataTable dt = new DataTable();
            Type type = typeof(T);
            List<PropertyInfo> properties = new List<PropertyInfo>();
            Array.ForEach<PropertyInfo>(type.GetProperties(), p =>
            {
                properties.Add(p);
                dt.Columns.Add(p.Name, p.PropertyType);
            });
            foreach (var item in list)
            {
                DataRow row = dt.NewRow();
                properties.ForEach(p =>
                {
                    row[p.Name] = p.GetValue(item, null);
                });
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
