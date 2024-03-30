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
            try
            {
            //HI|660088a8fa96991fcaad93f6|1916|11492|576|5|eraboss|1|9|0|228000|0|228000;228000|0;0|false|3|2|1|[0,0,0,0,0,0,0]|{"1":[],"2":[]}|null||11
            var userID = message.Split('|')[1];
            
            var x = SafeParse(message.Split("|")[2]);
            var y = SafeParse(message.Split("|")[3]);
            var speed = SafeParse(message.Split("|")[4]);
            //5 unk
            var name = message.Split('|')[6];
            session.User = new User(name , new(x , y));
            session.User.Speed = speed;
            session.User.ID = userID;
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static int SafeParse(string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            var index = value.IndexOf('.');
            if (index > 0)
            {
                value = value.Substring(0, index);
                if (int.TryParse(value, out result))
                {
                    return result;
                }
            }
            
            return 0;
        }

        public string Write()
        {
            throw new NotImplementedException();
        }
    }
}