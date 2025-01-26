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
                throw new Exception("No file found bunkbed-bom.json");
            }
            if (!File.Exists(routingFile))
            {
                throw new Exception("No file found bunkbed-routing.json");
            }

            var bomJson = File.ReadAllText(bomFile);
            var routingJson = File.ReadAllText(routingFile);

            var bom = JsonConvert.DeserializeObject<T1>(bomJson);
            if (bom == null )
            {
                throw new Exception("Deserialization failed. The BOM file contains invalid data.");
            }

            var routing = JsonConvert.DeserializeObject<List<T2>>(routingJson);
            if (routing == null)
            {
                throw new Exception("Deserialization failed. The Routing file contains invalid data.");
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