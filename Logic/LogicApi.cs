using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceAceToolEraAria.Connection;
using SpaceAceToolEraAria.Connection.Coomands.Read;

namespace SpaceAceToolEraAria.Logic
{
    public class LogicApi(Session session)
    {
        
        public void Logic()
        {
            Console.WriteLine("Logic");
        }
        //TODO: remove also players and handle players properly
        public async Task Start()
        {
            while(true)
            {
                try
                {
                    await Task.Delay(200);
                    if( session.User == null || !session.User.AnyNpc())
                    {
                        continue;
                    }
                    System.Console.WriteLine("Logic");
                    var closest = session.User.NpcClosestTo();
                    if(session.User.Target?.ID != closest.ID)
                    {
                        await session.SendPacket(new NpcClicked(closest.ID));
                        continue;
                    }
                        await session.SendPacket("SRTA");
                    }
                catch(Exception e)
                {
                    System.Console.WriteLine(e);
                }
            }
        }
    }
}