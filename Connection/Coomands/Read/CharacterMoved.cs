using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collections.Movement;

namespace SpaceAceToolEraAria.Connection.Coomands.Read
{
    public class CharacterMoved : IPacket
    {
        public const string ID = "SHM";
        //SHM|12|14888|9632|23534.32
        public void Read(Session session, string message)
        {
            try
            {
                var data = message.Split('|');
                var id = data[1];
                if(!int.TryParse(id, out _))
                {
                    return;
                }
                var x = int.Parse(data[2]);
                var y = int.Parse(data[3]);
                //remove everything after the dot also remove the dot but the dot not need to be there everytime
                var cleanTime = data[4];
                if(cleanTime.Contains('.')) 
                {
                    cleanTime = cleanTime.Substring(0, cleanTime.IndexOf('.'));
                }
                var time = double.Parse(cleanTime);
                var npc = session.User.GetNpc(id);
                if(npc != null)
                {
                    Movement.Move(npc, new(x, y), (int)time);
                 
                }
                else
                {
 
                }
            }
            catch (Exception ex)
            {
            }
         
        }

        public string Write()
        {
            throw new NotImplementedException();
        }
    }
}