
namespace BomRoutingApp {
    class BomItem
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Step { get; set; }
        public string Source { get; set; }
        public List<BomItem> Bom { get; set; }
    }

    class RoutingStep
    {
        public int Step { get; set; }
        public string Description { get; set; }
        public int TaktTime { get; set; }
    }
}