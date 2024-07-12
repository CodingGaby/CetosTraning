using Newtonsoft.Json;
using System;
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

        public string ConvertToJson()
        {
            Logger.WriteLog("Converting CSV file to JSON...");
            Logger.WriteLog("CSV File has been converted to JSON successfully.");
            return JsonConvert.SerializeObject(csvData, Formatting.Indented);
        }

        public static void FunctionToJson(List<Dictionary<string, string>> csvData, string outputPath) {
            var jsonConverter = new JSONConverter(csvData);
            var json = jsonConverter.ConvertToJson();
            Logger.WriteLog("Writing JSON file...");
            File.WriteAllText(outputPath, json);
            Logger.WriteLog("JSON file has been written successfully.");
        }
    }
}
