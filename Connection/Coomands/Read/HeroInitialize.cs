using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceAceToolEraAria.Connection;
using SpaceAceToolEraAria.Connection.Coomands;

namespace SpaceAceToolEraAria.Connection.Coomands.Read
{
    public class HeroInitialize : IPacket
    {
        public const string Id = "HI";
        public void Read(Session session, string message)
        {
            //HI|660088a8fa96991fcaad93f6|1916|11492|576|5|eraboss|1|9|0|228000|0|228000;228000|0;0|false|3|2|1|[0,0,0,0,0,0,0]|{"1":[],"2":[]}|null||11
            var userID = message.Split('|')[1];
            var x = int.Parse(message.Split('|')[2]);
            var y = int.Parse(message.Split('|')[3]);
            var speed = int.Parse(message.Split('|')[4]);
            //5 unk
            var name = message.Split('|')[6];
            session.User = new User(name , new(x , y));
            session.User.Speed = speed;
            session.User.ID = userID;
        }

        public string Write()
        {
            throw new NotImplementedException();
        }
    }
}