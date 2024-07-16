using System.Collections.Generic;
using System.IO;
using System;

namespace CSV_Converter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // PATHS
            string csvPath = @"";
            var jsonPath = @"C:\Users\ogane\Documents\prueba.json";
            var xmlPath = @"C:\Users\ogane\Documents\prueba.xml";

            try {
                Console.WriteLine("\nPlease enter CSV File path:");

                // Read path
                csvPath = Console.ReadLine();

                // Verify that the file exists
                if (!File.Exists(csvPath)) {
                    Console.WriteLine($"The file is not found in the specified path: {csvPath}");
                    return;
                }
            } catch (Exception ex) {
                Console.WriteLine($"There was an ERROR: {ex.Message}");
                Logger.WriteLog($"There was an ERROR: {ex.Message}");
            }

            bool convert = true;

            // Ask if the user wants to convert the document again
            while (convert) {
                // Read CSV file data
                var csvData = ReadCsvFile(csvPath);

                // Request output format
                Console.WriteLine("\nPlease select an output format: ");
                Console.WriteLine("1. JSON format");
                Console.WriteLine("2. XML format");
                Console.WriteLine("3. Both JSON and XML format");
                Console.WriteLine("4. Exit");
                var formatType = Console.ReadLine();

                // Select output format
                switch (formatType) {
                    case "1":
                        // Convert CSV data to JSON
                        var json = JSONConverter.ConvertToJson(csvData);
                        JSONConverter.WriteFile(jsonPath, json);
                        break;

                    case "2":
                        // Convert CSV data to XML
                        var xml = XMLConverter.ConvertToXml(csvData);
                        XMLConverter.WriteFile(xmlPath, xml);
                        break;

                    case "3":
                        // Convert CSV data to both JSON and XML
                        json = JSONConverter.ConvertToJson(csvData);
                        JSONConverter.WriteFile(jsonPath, json);
                        xml = XMLConverter.ConvertToXml(csvData);
                        XMLConverter.WriteFile(xmlPath, xml);
                        break;

                    case "4":
                        // Exit the application
                        convert = false;
                        continue;

                    default:
                        Console.WriteLine("Invalid selection. Please select 1, 2, 3, or 4.");
                        break;
                }

                if (convert) {
                    // Ask if the user wants to convert the file again
                    Console.WriteLine("\nDo you want to convert the file again? (y/n): ");
                    var continueChoice = Console.ReadLine();
                    if (continueChoice.ToLower() != "y") {
                        convert = false;
                    }
                }
            }
        }

        static List<Dictionary<string, string>> ReadCsvFile(string csvPath)
        {
            var csvData = new List<Dictionary<string, string>>();
            using (var reader = new StreamReader(csvPath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null) continue;

                    var cells = line.Split(',');
                    if (cells.Length >= 2)
                    {
                        var row = new Dictionary<string, string>();
                        string key = cells[0].Trim();
                        string value = cells[1].Trim('=', '"').Trim();
                        row[key] = value;
                        csvData.Add(row);
                    }
                }
            }
            Logger.WriteLog($"CSV File ({csvPath}) has been read successfully.");
            return csvData;
        }
    }
}