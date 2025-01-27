using BomRoutingApp;

try
{
    var sourceData = FileWorker.ReadSourceFiles<BomItem, RoutingStep>();

    Report report = new Report(
        (BomItem)sourceData["bom"],
        (List<RoutingStep>)sourceData["routing"]
    );

    report.GenerateSumQuantityReport();

    report.DisplayNoProvidedComponents();

    report.DisplayOverallTaktTime();


}
catch (System.Exception ex)
{
    ConsoleMessage.DisplayError($"Error: {ex.Message}");
    Environment.Exit(1);
}


