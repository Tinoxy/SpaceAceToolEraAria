using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceAceToolEraAria.Connection.Coomands.Read
{
    public class RemoveBox : IPacket
    {
        public const string ID = "CLST";
        public void Read(Session session, string message)
        {
            //CLST|0|43
            var data = message.Split('|');
            var id = data[1];
            session.User.RemoveBox(id);
        }

        public string Write()
        {
            throw new NotImplementedException();
        }
    }
}