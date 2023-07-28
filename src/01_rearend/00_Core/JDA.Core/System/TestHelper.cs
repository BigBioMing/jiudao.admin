using JDA.Core.System.Implements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Runtime
{/// <summary>
 /// 获取程序运行信息的类
 /// </summary>
    public static class EnvironmentInfo
    {
        #region 获取信息
        /// <summary>
        /// 获取程序运行资源信息
        /// </summary>
        /// <returns></returns>
        public static (string, List<KeyValuePair<string, object>>) GetApplicationRunInfo()
        {
            DefaultApplicationRuntimeInformation info = new DefaultApplicationRuntimeInformation();
            return GetValues(info);
        }

        /// <summary>
        /// 获取系统运行平台信息
        /// </summary>
        /// <returns></returns>
        public static (string, List<KeyValuePair<string, object>>) GetSystemPlatformInfo()
        {
            DefaultSystemPlatformInformation info = new DefaultSystemPlatformInformation();
            return GetValues(info);
        }

        /// <summary>
        /// 获取系统运行环境信息
        /// </summary>
        /// <returns></returns>
        public static (string, List<KeyValuePair<string, object>>) GetSystemRunEvnInfo()
        {
            DefaultSystemRuntimeEnvironmentInformation info = new DefaultSystemRuntimeEnvironmentInformation(new DefaultMachineInformation());
            return GetValues(info);
        }

        /// <summary>
        /// 获取系统全部环境变量
        /// </summary>
        /// <returns></returns>
        public static (string, List<KeyValuePair<string, object>>) GetEnvironmentVariables()
        {
            List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
            IDictionary environmentVariables = Environment.GetEnvironmentVariables();
            foreach (DictionaryEntry de in environmentVariables)
            {
                list.Add(new KeyValuePair<string, object>(de.Key.ToString(), de.Value));
            }
            return ("系统环境变量", list);
        }

        #endregion

        #region 工具

        /// <summary>
        /// 获取某个类型的值以及名称
        /// </summary>
        /// <typeparam name="TInfo"></typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        private static (string, List<KeyValuePair<string, object>>) GetValues<TInfo>(TInfo info)
        {
            List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
            Type type = info.GetType();
            PropertyInfo[] pros = type.GetProperties();
            foreach (var item in pros)
            {
                var name = GetDisplayNameValue(item.GetCustomAttributesData());
                var value = GetPropertyInfoValue(item, info);
                list.Add(new KeyValuePair<string, object>(name, value));
            }
            return
                (GetDisplayNameValue(info.GetType().GetCustomAttributesData()),
                list);
        }

        /// <summary>
        /// 获取 [Display] 特性的属性 Name 的值
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        private static string GetDisplayNameValue(IList<CustomAttributeData> attrs)
        {
            var arguments = attrs.FirstOrDefault(x => x.AttributeType.Name == nameof(DescriptionAttribute)).NamedArguments;
            if(arguments != null && arguments.Count > 0)
            {
                var argument = arguments.FirstOrDefault(x => x.MemberName == nameof(DescriptionAttribute.Description));
                return argument.TypedValue.Value.ToString();
            }
            else
            {
                var arguments2 = attrs.FirstOrDefault(x => x.AttributeType.Name == nameof(DescriptionAttribute)).ConstructorArguments;
                var argument2 = arguments2.FirstOrDefault();
                return argument2.Value.ToString();
            }
        }

        /// <summary>
        /// 获取属性的值
        /// </summary>
        /// <param name="info"></param>
        /// <param name="obj">实例</param>
        /// <returns></returns>
        private static object GetPropertyInfoValue(PropertyInfo info, object obj)
        {
            return info.GetValue(obj);
        }

        #endregion
    }
}
