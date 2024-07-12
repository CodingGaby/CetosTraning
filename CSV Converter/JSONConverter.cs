using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSV_Converter
{
    internal class JSONConverter
    {
        private List<Dictionary<string, string>> csvData;

        public JSONConverter(List<Dictionary<string, string>> csvDatap)
        {
            csvData = csvDatap;
        }

        public static string ConvertToJson(List<Dictionary<string, string>> csvData) {
            Logger.WriteLog("Converting CSV file to JSON...");
            var jsonConverter = new JSONConverter(csvData);
            Logger.WriteLog("CSV File has been converted to JSON successfully.");
            return JsonConvert.SerializeObject(csvData, Formatting.Indented);
        }
      
        public static void WriteFile(string filePath, string json) {
            Logger.WriteLog("Writing JSON file...");
            File.WriteAllText(filePath, json);
            Logger.WriteLog("JSON file has been written successfully.");
        }
    }
}
