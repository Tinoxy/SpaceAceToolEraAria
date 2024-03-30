using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceAceToolEraAria.Connection.Coomands.Read
{
    public class RemoveCharacter : IPacket
    {
        public const string ID = "SD";
        public void Read(Session session, string message)
        {
            //SD|0|43
            var data = message.Split('|');
            var id = data.Last();
            session.User.RemoveNpc(id);
        }
        public string Write()
        {
            throw new NotImplementedException();
        }
    }
    
}