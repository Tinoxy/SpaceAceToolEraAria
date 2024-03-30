using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collections.Movement;

namespace SpaceAceToolEraAria.Connection.Coomands.Read
{
    public class HeroMoved : IPacket
    {
        public const string ID = "MD";
        public void Read(Session session, string message)
        {
            var data = message.Split('|');
            var x = int.Parse(data[1]);
            var y = int.Parse(data[2]);
            Movement.Move(session.User, new(x, y));
        }

        public string Write()
        {
            throw new NotImplementedException();
        }
    }
}