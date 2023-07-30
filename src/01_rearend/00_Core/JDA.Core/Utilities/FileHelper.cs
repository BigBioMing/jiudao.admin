using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Utilities
{
    public class FileHelper
    {
        public static byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public static void Delete(string path)
        {
            File.Delete(path);
        }
    }
}
