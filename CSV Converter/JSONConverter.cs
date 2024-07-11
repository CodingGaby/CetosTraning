using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
