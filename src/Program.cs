using BomRoutingApp;

try
{
    var sourceData = FileWorker.ReadSourceFiles<BomItem, RoutingStep>();

    var bom = (BomItem)sourceData["bom"];
    var routing = (List<RoutingStep>)sourceData["routing"];

    var report = new Report(bom, routing);

    report.GenerateSumQuantityReport();

    
}
catch (System.Exception ex)
{
    ConsoleMessage.DisplayError($"Error: {ex.Message}");
    Environment.Exit(1);
}


