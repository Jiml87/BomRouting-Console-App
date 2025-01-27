using System.Text;

namespace BomRoutingApp
{
    class Report
    {
        public readonly BomItem bomData;
        public readonly List<RoutingStep> routingData;

        public Report(BomItem _bomData, List<RoutingStep> _routingData)
        {
            bomData = _bomData;
            routingData = _routingData;
        }

        private void IterateBomData(Action<BomItem> callback)
        {
            Stack<BomItem> stack = new Stack<BomItem>(new List<BomItem> { bomData });

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

        public void GenerateSumQuantityReport()
        {
            Dictionary<string, int> providedComponents = new Dictionary<string, int>();

            IterateBomData((BomItem item) =>
            {
                if (item.source == "Provided")
                {
                    if (providedComponents.ContainsKey(item.description))
                    {
                        providedComponents[item.description] += item.quantity;
                    }
                    else
                    {
                        providedComponents[item.description] = item.quantity;
                    }
                }
            });

            FileWorker.WriteOutputFile(providedComponents);
        }

        public void DisplayNoProvidedComponents()
        {
            HashSet<int> bomSetByStep = new HashSet<int> { };

            IterateBomData((BomItem item) =>
            {
                bomSetByStep.Add(item.step);
            });

            ConsoleMessage.DisplaySuccess("No provided components:");

            string result = routingData.Aggregate(new StringBuilder(), (acc, item) =>
            {
                if (!bomSetByStep.Contains(item.step))
                {
                    acc.Append($" - Step {item.step} '{item.description}' has no provided components added.\n");
                }
                return acc;
            }).ToString();

            Console.WriteLine(result);
        }

        public void DisplayOverallTaktTime()
        {
            int totalTaktTime = 0;
            IterateBomData((BomItem item) =>
            {
                totalTaktTime += item.quantity;
            });

            ConsoleMessage.DisplaySuccess($"Overall takt time: {totalTaktTime} minutes.\n");
        }
    }
}