// See https://aka.ms/new-console-template for more information
using SpaceAceToolEraAria.Connection;

Console.WriteLine("Hello, World!");

await Task.Run(WebSocketServer.Start);
Console.ReadLine();
