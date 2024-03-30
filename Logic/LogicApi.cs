using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collections.Movement;
using SpaceAceToolEraAria.Connection;
using SpaceAceToolEraAria.Connection.Coomands.Read;

namespace SpaceAceToolEraAria.Logic
{
    public class LogicApi(Session session)
    {
        public async Task Start()
        {
            while(true)
            {
                try
                {
                    await Task.Delay(200);
                    if( session.User == null)
                    {
                        continue;
                    }
                    var closest = session.User.NpcClosestTo();
                    System.Console.WriteLine(closest);
                    if(closest == null)
                    {
                        if(session.User.Moving)
                            continue;
                        var random = new Random();
                        var x = random.Next(0, 20000);
                        var y = random.Next(0, 13000);
                        await session.SendPacket("MD|"+x+"|"+y);
                        var timeToEnd = session.User.Position.DistanceTo(new(x , y)) / session.User.Speed * 1000;
                        await session.SendClient($"SHM|{session.User.ID}|{x}|{y}|{(int)timeToEnd}");
                    }
                    if(closest != null && session.User.Target?.ID != closest.ID)
                    {
                        await session.SendPacket(new NpcClicked(closest.ID));
                        await session.SendPacket("SRTA");
                        session.User.Target = closest;
                        continue;
                    }
                    if(closest == null)
                        continue;
                        await Task.Delay(120);
                        await session.SendPacket($"MD|{closest.Position.X + 400}|{closest.Position.Y}");
                        var time = session.User.Position.DistanceTo(closest.Position) / session.User.Speed * 1000;
                        if(time < 300)
                        {
                            time = 500;
                        }
                        await session.SendClient($"SHM|{session.User.ID}|{closest.Position.X + 400}|{closest.Position.Y}|{(int)time}");
                    }
                catch(Exception e)
                {
                    System.Console.WriteLine(e);
                }
            }
        }
    }
}