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
        private List<CsvRow> csvData;

        public XMLConverter(List<Dictionary<string, string>> csvDatap)
        {
            csvData = ConvertToCsvRows(csvDatap);
        }

        private List<CsvRow> ConvertToCsvRows(List<Dictionary<string, string>> csvDatap)
        {
            var rows = new List<CsvRow>();

            foreach (var dict in csvDatap)
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

        public string ConvertToXml()
        {
            Logger.WriteLog("Converting CSV file to XML...");
            var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(List<CsvRow>));

            serializer.Serialize(stringWriter, csvData);
            Logger.WriteLog("CSV File has been converted to XML successfully.");
            return stringWriter.ToString();
        }
    }
}
