// See https://aka.ms/new-console-template for more information
using SpaceAceToolEraAria.Connection;

System.Console.WriteLine("Enter profile name (npc , box) only npcs on x1 and x2 maps are supported");
var profile = Console.ReadLine();
Session.SetLogic(profile);
System.Console.WriteLine("Bot is ready!");
await Task.Run(WebSocketServer.Start);



System.Console.WriteLine("Press any key to exit");
Console.ReadLine();
