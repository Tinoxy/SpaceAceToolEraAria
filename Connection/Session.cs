using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using SpaceAceToolEraAria.Connection.Coomands;
using SpaceAceToolEraAria.Logic;
using SpaceAceToolEraAria.Tick;

namespace SpaceAceToolEraAria.Connection
{
    public class Session
    {
        public Guid Id { get; set; }
        public User User { get; internal set; }
        public static TickManager TickManager { get; set; } = new();
        public LogicApi Logic { get; set; }

        public Session(Guid id)
        {
            Id = id;
            Logic = new(this);
            _ = Task.Run(Logic.Start);
        }
        public async Task SendClient(string message) => await WebSocketServer.SendClient(Id, message);
        public async Task SendClient(IPacket packet) => await SendClient(packet.Write());
        public async Task SendPacket(string message) => await WebSocketServer.SendPacket(Id, message);
        public async Task SendPacket(IPacket packet) => await SendPacket(packet.Write());
        public async Task ReceivePacket(string message)
            =>  Parser.Parse(this, message);
        
        
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}