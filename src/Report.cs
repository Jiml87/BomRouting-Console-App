namespace BomRoutingApp
{
    static class Report
    {
        public static void GetSumQuantity(BomItem item, Dictionary<string, int> providedComponents)
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

            foreach (var subItem in item.bom)
            {
                GetSumQuantity(subItem, providedComponents);
            }
        }
    }
}