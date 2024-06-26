using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace CSV_Converter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var csvPath = @"C:\Users\macma\Desktop\supermarket.csv";
            var jsonPath = @"C:\Users\macma\Desktop\supermarket.json";

            //Leer datos de archivo CSV
            var csvData = ReadCsvFile(csvPath);

            //Convertir datos de CSV a JSON
            var json = ConvertToJson(csvData);

            File.WriteAllText(jsonPath, json);
            Console.WriteLine("CSV has been converted to JSON successfully.");
        }

        static List<Dictionary<string, string>> ReadCsvFile(string csvPath)
        {
            var csvData = new List<Dictionary<string, string>>();
            using (var reader = new StreamReader(csvPath))
            {
                var headers = reader.ReadLine()?.Split(',').ToList();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null) continue;

                    var cells = line.Split(',').ToList();
                    if (RowHasData(cells))
                    {
                        var row = new Dictionary<string, string>();
                        for (int i = 0; i < headers.Count; i++)
                        {
                            row[headers[i]] = cells.ElementAtOrDefault(i) ?? string.Empty;
                        }
                        csvData.Add(row);
                    }
                }
            }
            return csvData;
        }

        static bool RowHasData(List<string> cells)
        {
            return cells.Any(x => x.Length > 0);
        }

        static string ConvertToJson(List<Dictionary<string, string>> csvData)
        {
            return JsonConvert.SerializeObject(csvData, Formatting.Indented);
        }
    }
}
