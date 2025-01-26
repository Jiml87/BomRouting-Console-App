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
            if (!File.Exists(bomFile) || !File.Exists(routingFile))
            {
                throw new Exception("No file found bunkbed-bom.json or bunkbed-routing.json");
            }

            var bomJson = File.ReadAllText(bomFile);
            var routingJson = File.ReadAllText(routingFile);

            var bom = JsonConvert.DeserializeObject<T1>(bomJson);
            var routing = JsonConvert.DeserializeObject<List<T2>>(routingJson);

            if (bom == null || routing == null)
            {
                throw new Exception("Deserialization failed. One or both of the files contain invalid data.");
            }

            var result = new Dictionary<string, object>
            {
                { "bom", bom },
                { "routing", routing }
            };

            return result;

        }

        public static void WriteOutputFile()
        {

        }
    }
}