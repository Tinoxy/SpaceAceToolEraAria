using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SpaceAceToolEraAria.Tick;
using WatsonWebsocket;

namespace SpaceAceToolEraAria.Connection
{
    public static class WebSocketServer
    {
        public static WatsonWsServer Server { get; set; }= new WatsonWsServer("localhost" , 8999 , false);

        public static async Task Start()
        {
            Server.ClientDisconnected += (s, e) =>
            {
                if(e.Client.Guid == GameSession?.Id)
                {
                    GameSession.Dispose();
                    GameSession = null;
                }
            };
            Server.MessageReceived += OnMessageRequest;
            _ =Task.Run(async() => 
            {
                while(true)
                {
                   await Session.TickManager.Tick();
                }
            });
            await Server.StartAsync();
        }
        public static Session? GameSession { get; set; }

        public static async Task SendPacket(Guid guid, string message) => await Server.SendAsync(guid, "send&" +message);	
        public static async Task SendClient(Guid guid, string message) => await Server.SendAsync(guid, "receive&" + message);


        private static void OnMessageRequest(object? sender, MessageReceivedEventArgs e)
        {
            var client = e.Client.Guid;
            var message = Encoding.UTF8.GetString(e.Data);
            if(message.StartsWith("HI|"))
            {
                GameSession = new Session(client);  
                System.Console.WriteLine("Game client connected");
            }
            if(GameSession != null)
            {
                _ = GameSession.ReceivePacket(message);
            }
        }

     
    }
}