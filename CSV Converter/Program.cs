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

            ExecuteWithExceptionHandling(() =>
            {
                Console.WriteLine("\nPlease enter CSV File path:");

                // Read path
                csvPath = Console.ReadLine();
                Logger.WriteLog($"User entered CSV File path: {csvPath}");

                // Verify that the file exists
                if (!File.Exists(csvPath))
                {
                    Console.WriteLine($"The file is not found in the specified path: {csvPath}");
                    Logger.WriteLog($"The file is not found in the specified path: {csvPath}");
                    return;
                }
            });

            bool convert = true;

            // Ask if the user wants to convert the document again
            while (convert)
            {
                // Read CSV file data
                var csvData = ReadCsvFile(csvPath);

                // Request output format
                Console.WriteLine("\nPlease select an output format: ");
                Console.WriteLine("1. JSON format");
                Console.WriteLine("2. XML format");
                Console.WriteLine("3. Both JSON and XML format");
                Console.WriteLine("4. Exit");
                var formatType = Console.ReadLine();
                Logger.WriteLog($"User selected output format option: {formatType}");

                var jsonPath = @"";
                var xmlPath = @"";

                // Select output format
                switch (formatType)
                {
                    case "1":
                        ExecuteWithExceptionHandling(() =>
                        {
                            //Pedir el path de salida del archivo
                            Console.WriteLine("\nPlease enter the path where you want to save the JSON file:");
                            var jsonDirectory = Console.ReadLine();
                            Logger.WriteLog($"User entered JSON file path: {jsonDirectory}");

                            if (!Directory.Exists(jsonDirectory))
                            {
                                Console.WriteLine($"The directory does not exist: {jsonDirectory}");
                                Logger.WriteLog($"The directory does not exist: {jsonDirectory}");
                                return;
                            }

                            //Pedir el nombre de salida del archivo y añadir la extensión
                            Console.WriteLine("\nPlease enter the file name for the JSON file (without extension):");
                            var jsonFileName = Console.ReadLine();
                            Logger.WriteLog($"User entered JSON file name: {jsonFileName}");
                            jsonPath = Path.Combine(jsonDirectory, jsonFileName + ".json");

                            // Convert CSV data to JSON
                            var json = JSONConverter.ConvertToJson(csvData);
                            JSONConverter.WriteFile(jsonPath, json);
                        });
                        break;

                    case "2":
                        ExecuteWithExceptionHandling(() =>
                        {
                            Console.WriteLine("\nPlease enter the path where you want to save the XML file:");
                            var xmlDirectory = Console.ReadLine();
                            Logger.WriteLog($"User entered XML file path: {xmlDirectory}");

                            if (!Directory.Exists(xmlDirectory))
                            {
                                Console.WriteLine($"The directory does not exist: {xmlDirectory}");
                                Logger.WriteLog($"The directory does not exist: {xmlDirectory}");
                                return;
                            }

                            Console.WriteLine("\nPlease enter the file name for the XML file (without extension):");
                            var xmlFileName = Console.ReadLine();
                            xmlPath = Path.Combine(xmlDirectory, xmlFileName + ".xml");

                            // Convert CSV data to XML
                            var xml = XMLConverter.ConvertToXml(csvData);
                            Logger.WriteLog($"User entered XML file name: {xmlFileName}");
                            XMLConverter.WriteFile(xmlPath, xml);
                        });
                        break;

                    case "3":
                        ExecuteWithExceptionHandling(() =>
                        {
                            Console.WriteLine("\nPlease enter the directory where you want to save the JSON and XML files:");
                            var baseDirectory = Console.ReadLine();
                            Logger.WriteLog($"User entered base directory for JSON and XML files: {baseDirectory}");

                            if (!Directory.Exists(baseDirectory))
                            {
                                Console.WriteLine($"The directory does not exist: {baseDirectory}");
                                Logger.WriteLog($"The directory does not exist: {baseDirectory}");
                                return;
                            }

                            Console.WriteLine("\nPlease enter the base file name for both JSON and XML files (without extension):");
                            var baseFileName = Console.ReadLine();
                            Logger.WriteLog($"User entered base file name for both JSON and XML files: {baseFileName}");
                            jsonPath = Path.Combine(baseDirectory, baseFileName + ".json");
                            xmlPath = Path.Combine(baseDirectory, baseFileName + ".xml");

                            var json = JSONConverter.ConvertToJson(csvData);
                            JSONConverter.WriteFile(jsonPath, json);

                            var xml = XMLConverter.ConvertToXml(csvData);
                            XMLConverter.WriteFile(xmlPath, xml);
                        });
                        break;

                    case "4":
                        // Exit the application
                        Logger.WriteLog("User chose to exit the application.");
                        convert = false;
                        continue;

                    default:
                        Console.WriteLine("Invalid selection. Please select 1, 2, 3, or 4.");
                        Logger.WriteLog($"Invalid selection: {formatType}");
                        break;
                }

                if (convert)
                {
                    // Ask if the user wants to convert the file again
                    Console.WriteLine("\nDo you want to convert the file again? (y/n): ");
                    var continueChoice = Console.ReadLine();
                    Logger.WriteLog($"User chose to convert the file again: {continueChoice}");
                    if (continueChoice.ToLower() != "y")
                    {
                        convert = false;
                    }
                }
            }
        }

        static void ExecuteWithExceptionHandling(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an ERROR: {ex.Message}");
                Logger.WriteLog($"There was an ERROR: {ex.Message}");
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
