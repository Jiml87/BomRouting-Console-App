using BomRoutingApp;

try
{
    var result = FileWorker.ReadSourceFiles<BomItem, RoutingStep>();

    var bom = (BomItem)result["bom"];
    var routing = (List<RoutingStep>)result["routing"];

    Console.WriteLine($"Bom Item: {bom.description}, Quantity: {bom.quantity}");
    Console.WriteLine($"Routing Step: {routing[0].step}, Description: {routing[0].description}");
}
catch (System.Exception ex)
{
    ConsoleMessage.DisplayError($"Error: {ex.Message}");
    Environment.Exit(1);
}


