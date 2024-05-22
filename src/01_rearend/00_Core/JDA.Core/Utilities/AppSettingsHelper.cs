using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Utilities
{
    public class AppSettingsHelper
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string? GetConfig(string key)
        {
            if (_configuration == null)
            {
                throw new InvalidOperationException("You must call Initialize() method first.");
            }

            return _configuration.GetSection(key)?.Value;
        }
    }
}
