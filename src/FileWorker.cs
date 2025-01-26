using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BomRoutingApp
{
    static class FileWorker 
    {
        public static Dictionary<string, object> ReadSourceFiles<T1, T2>(
            string bomFile = "mocks/bunkbed-bom.json",
            string routingFile = "mocks/bunkbed-routing.json"
        ) 
        {
            if (!File.Exists(bomFile))
            {
                throw new Exception($"No file found \"{bomFile}\"");
            }
            if (!File.Exists(routingFile))
            {
                throw new Exception($"No file found \"{routingFile}\"");
            }

            var bomJson = File.ReadAllText(bomFile);
            var routingJson = File.ReadAllText(routingFile);

            var bom = JsonConvert.DeserializeObject<T1>(bomJson);
            if (bom == null )
            {
                throw new Exception($"Deserialization failed. The file \"{bomFile}\" contains invalid data.");
            }

            var routing = JsonConvert.DeserializeObject<List<T2>>(routingJson);
            if (routing == null)
            {
                throw new Exception($"Deserialization failed. The file \"{routingFile}\" contains invalid data.");
            }

            var result = new Dictionary<string, object>
            {
                { "bom", bom },
                { "routing", routing }
            };

            return result;

        }

        public static void WriteOutputFile(Dictionary<string, int> providedComponents, string outputFilePath = "output.csv")
        {
            using (var writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("component,quantity");
                foreach (var component in providedComponents)
                {
                    writer.WriteLine($"{component.Key},{component.Value}");
                }
            }
            
            ConsoleMessage.DisplaySuccess($"File \"{outputFilePath}\" created successfully!\n");
        }
    }
}