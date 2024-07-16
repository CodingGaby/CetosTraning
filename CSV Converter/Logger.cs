using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Converter
{
    public static class Logger
    {
        private static readonly string logPath = Path.Combine(Path.GetTempPath(), "CSV_Converter", "log.txt");

        static Logger()
        {
            var logDirectory = Path.GetDirectoryName(logPath);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }

        public static void WriteLog(string message)
        {
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine($"{DateTime.Now} ---LOG---: {message}");
            }
        }
    }
}
