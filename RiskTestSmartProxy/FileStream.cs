using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RiskTestSmartProxy
{
    public static class FileStream
    {
        public static string GetContent(string path)
        {
            if(File.Exists(path))
            {
                string content = File.ReadAllText(path);
                return content;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string[] GetContentLines(string path)
        {
            if (File.Exists(path))
            {
                string[] content = File.ReadAllLines(path);
                return content;
            }
            else
            {
                return null;
            }
        }
    }
}
