using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceAceToolEraAria.Collection.Characters;

namespace SpaceAceToolEraAria.Connection.Coomands.Read
{
    public class CharacterInitialize : IPacket
    {
        public const string ID = "SS";
        public void Read(Session session, string message)
        {
            //SS|39|3408|9968|35|-=[Mordon]=-|0|0|20000|10000|20000;20000|10000;10000|false|0|1|false|1|[0,0,0,0,0,0,0]|{"1":[],"2":[]}|null0
            //SD|0|39
            var data = message.Split('|');
            var id = data[1];
            var x = int.Parse(data[2]);
            var y = int.Parse(data[3]);
            var name = data[5];
            var isNpc = data[6] == "1";
            var npc = new Npc(name, new(x, y))
            {
                ID = id
            };
            session.User.AddNpc(npc);
        }

        public string Write()
        {
            throw new NotImplementedException();
        }
    }
}