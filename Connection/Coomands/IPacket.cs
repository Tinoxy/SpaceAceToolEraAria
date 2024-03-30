using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceAceToolEraAria.Connection.Coomands
{
    public interface IPacket
    {
        public void Read(Session session, string message);
        public string Write();
    }
}