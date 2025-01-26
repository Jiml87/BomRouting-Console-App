using System.Text;

namespace BomRoutingApp
{
    class Report
    {
        public readonly BomItem bomData;
        public readonly List<RoutingStep> routingData;
        private  Dictionary<string, int> _providedComponents = new Dictionary<string, int>();

        public Report(BomItem _bomData, List<RoutingStep>_routingData)
        {
            bomData = _bomData;
            routingData = _routingData;
        }

        private void GetSumQuantity(BomItem item)
        {
            if (item.source == "Provided")
            {
                if (_providedComponents.ContainsKey(item.description))
                {
                    _providedComponents[item.description] += item.quantity;
                }
                else
                {
                    _providedComponents[item.description] = item.quantity;
                }
            }

            foreach (var subItem in item.bom)
            {
                GetSumQuantity(subItem);
            }
        }
        public void GenerateSumQuantityReport()
        {
            GetSumQuantity(bomData);

            FileWorker.WriteOutputFile(_providedComponents);
        }

        private void IterateBomData(Action<BomItem> callback)
        {
            var stack = new Stack<BomItem>(new List<BomItem> { bomData });

            while (stack.Count > 0)
            {
                var item = stack.Pop();
                callback(item);

                foreach (var subItem in item.bom)
                {
                    stack.Push(subItem);
                }
            }
        }

        public void DisplayNoProvidedComponents()
        {
            HashSet<int> _bomSetByStep = new HashSet<int>{};

            IterateBomData((BomItem item) => {
                    _bomSetByStep.Add(item.step);
            });

            ConsoleMessage.DisplaySuccess("No provided components:");

            string result = routingData.Aggregate(new StringBuilder(), (acc, item) => {
                if(!_bomSetByStep.Contains(item.step))
                {
                    acc.Append($" - Step {item.step} '{item.description}' has no provided components added.\n");
                }
                return acc;
            }).ToString();

            Console.WriteLine(result);
        }

        public void DisplayOverallTaktTime()
        {
            var totalTaktTime = routingData.Sum(step => step.taktTime);
            ConsoleMessage.DisplaySuccess($"Overall takt time: {totalTaktTime} minutes.\n");
        }
    }
}