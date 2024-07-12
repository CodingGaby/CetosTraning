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

            bool convert = true;
            
            //Preguntar si desea convertir nuevamente el documento
            while (convert) 
            {
                //Leer datos de archivo CSV
                var csvData = ReadCsvFile(csvPath);

                //Solicitar formato de salida
                Console.WriteLine("\nPlease select an output format. (press 1 for a JSON format or press 2 for an XML format) ");
                var formatType = Console.ReadLine();

                //Seleccionar formato de salida
                switch (formatType) {
                    //Convertir datos de CSV a JSON
                    case "1":

                        JSONConverter JSONConv = new JSONConverter(csvData);
                        var json = JSONConv.ConvertToJson();
                        File.WriteAllText(jsonPath, json);
                        Console.WriteLine("CSV has been converted to JSON successfully.");
                        break;

                    //Convertir datos de CSV a XML
                    case "2":

                        //Convertir datos de CSV a XML
                        XMLConverter XMLConv = new XMLConverter(csvData);
                        var xml = XMLConv.ConvertToXml();
                        Logger.WriteLog("Writing XML file...");
                        File.WriteAllText(xmlPath, xml);
                        Logger.WriteLog("XML file has been written successfully.");
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Please select 1 or 2.");
                        break;
                        continue;
                }

                // Preguntar si desea convertir el archivo de nuevo
                Console.WriteLine("\nDo you want to convert the file again? (y/n): ");
                var continueChoice = Console.ReadLine();
                if (continueChoice.ToLower() != "y") 
                {
                    convert = false;
                }
            }
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
