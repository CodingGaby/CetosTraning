using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CSV_Converter
{
    public class CsvRow
    {
        public List<CsvColumn> Columns { get; set; }
    }

    public class CsvColumn
    {
        [XmlAttribute]
        public string Key { get; set; }
        [XmlText]
        public string Value { get; set; }
    }

    internal class XMLConverter
    {
        private static List<CsvRow> ConvertToCsvRows(List<Dictionary<string, string>> csvData)
        {
            var rows = new List<CsvRow>();

            foreach (var dict in csvData)
            {
                var row = new CsvRow { Columns = new List<CsvColumn>() };

                foreach (var kvp in dict)
                {
                    row.Columns.Add(new CsvColumn { Key = kvp.Key, Value = kvp.Value });
                }

                rows.Add(row);
            }

            return rows;
        }

        public static string ConvertToXml(List<Dictionary<string, string>> csvData)
        {
            Logger.WriteLog("Converting CSV file to XML...");
            var rows = ConvertToCsvRows(csvData);
            var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(List<CsvRow>));

            serializer.Serialize(stringWriter, rows);
            Logger.WriteLog("CSV File has been converted to XML successfully.");
            return stringWriter.ToString();
        }

        public static void WriteFile(string filePath, string xml)
        {
            Logger.WriteLog("Writing XML file...");
            File.WriteAllText(filePath, xml);
            Logger.WriteLog("XML file has been written successfully.");
        }
    }
}
