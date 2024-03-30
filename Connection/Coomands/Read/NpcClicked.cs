using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceAceToolEraAria.Collection.Characters;

namespace SpaceAceToolEraAria.Connection.Coomands.Read
{
    public class NpcClicked : IPacket
    {
        public const string ID = "RT";
        public string Id { get; set; }
        public NpcClicked()
        {

        }

        public NpcClicked(string id)
        {
            Id = id;
        }
        public void Read(Session session, string message)
        {
            var data = message.Split('|');
            var npc = session.User.GetNpc(data[1]);
            if(npc != null)
            {
                session.User.Target = npc;
                System.Console.WriteLine($"Npc {npc.ID}:{npc.Username} clicked");
            }
            else
            {
                System.Console.WriteLine($"Npc {data[1]} not found");
            }
        }
        public string Write()
        {
            var packet = $"{ID}|{Id}";
            return packet;
        }
    }
}