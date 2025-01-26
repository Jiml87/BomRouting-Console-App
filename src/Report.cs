namespace BomRoutingApp
{
    class Report
    {
        private  Dictionary<string, int> _providedComponents = new Dictionary<string, int>();
        public BomItem bomData;
        public List<RoutingStep> routingData;
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


    }
}