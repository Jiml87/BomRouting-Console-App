
namespace BomRoutingApp {
    class BomItem
    {
        public required string description { get; set; }
        public int quantity { get; set; }
        public int step { get; set; }
        public required string source { get; set; }
        public required List<BomItem> bom { get; set; }
    }

    class RoutingStep
    {
        public int step { get; set; }
        public required string description { get; set; }
        public int taktTime { get; set; }
    }
}