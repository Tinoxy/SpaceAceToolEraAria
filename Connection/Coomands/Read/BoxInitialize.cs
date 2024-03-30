using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceAceToolEraAria.Collection.Collectables;

namespace SpaceAceToolEraAria.Connection.Coomands.Read
{
    public class BoxInitialize : IPacket
    {
        public const string ID = "CLDP";
        public void Read(Session session, string message)
        {
            var data = message.Split('|');
            var x = int.Parse(data[1]);
            var y = int.Parse(data[2]);
            var id = data[4];
            var box = new Box(id , new(x, y));
            session.User.AddBox(box);

        }

        public string Write()
        {
            throw new NotImplementedException();
        }
    }
}