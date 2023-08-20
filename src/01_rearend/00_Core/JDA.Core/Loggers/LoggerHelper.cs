using JDA.Core.Loggers.Abstractions;
using JDA.Core.Loggers.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Loggers
{
    public class LoggerHelper
    {
        public static ILogger Default = new Logger();
    }
}
