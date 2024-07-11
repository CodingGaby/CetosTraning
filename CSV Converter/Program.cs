using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace CSV_Converter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //PATHS
            var csvPath = @"C:\Users\macma\Desktop\T628395-tcm850-240604-00003-17-e9715984-5faf-4dbd-bd31-899f9d51de8c.csv";
            var jsonPath = @"C:\Users\macma\Desktop\Res.json";
            var xmlPath = @"C:\Users\macma\Desktop\Res.xml";

            //Leer datos de archivo CSV
            var csvData = ReadCsvFile(csvPath);

            //Convertir datos de CSV a JSON
            JSONConverter JSONConv = new JSONConverter(csvData);
            var json = JSONConv.ConvertToJson();
            Logger.WriteLog("Writing JSON file...");
            File.WriteAllText(jsonPath, json);
            Logger.WriteLog("JSON file has been written successfully.");

            //Convertir datos de CSV a XML
            XMLConverter XMLConv = new XMLConverter(csvData);
            var xml = XMLConv.ConvertToXml();
            Logger.WriteLog("Writing XML file...");
            File.WriteAllText(xmlPath, xml);
            Logger.WriteLog("XML file has been written successfully.");
        }

        static List<Dictionary<string, string>> ReadCsvFile(string csvPath)
        {
            var csvData = new List<Dictionary<string, string>>();
            using (var reader = new StreamReader(csvPath))
            {
                //var headers = reader.ReadLine()?.Split(',').ToList();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null) continue;

                    var cells = line.Split(',');
                    if (cells.Length >= 2)
                    //if (RowHasData(cells))
                    {
                        var row = new Dictionary<string, string>();
                        string key = cells[0].Trim();
                        string value = cells[1].Trim('=', '"').Trim();
                        row[key] = value;
                        //for (int i = 0; i < headers.Count; i++)
                        //{
                        //    row[headers[i]] = cells.ElementAtOrDefault(i) ?? string.Empty;
                        //}
                        csvData.Add(row);
                    }
                }
            }
            Logger.WriteLog($"CSV File ({csvPath}) has been read successfully.");
            return csvData;
        }

        static bool RowHasData(List<string> cells)
        {
            return cells.Any(x => x.Length > 0);
        }
    }
}
