using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Utilities
{
    /// <summary>
    /// 时间帮助类
    /// </summary>
    public class DateTimeHelper
    {
        public static readonly DateTime UNIX_START_TIME = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 时间转时间戳（13位时间戳，毫秒级）
        /// </summary>
        /// <param name="dt">待转换的时间</param>
        /// <returns>13位时间戳，毫秒级</returns>
        public static long ExtractDateTimeToUnixTimestamp(DateTime dt)
        {
            return (long)dt.ToUniversalTime().Subtract(UNIX_START_TIME).TotalMilliseconds;
        }
    }
}
