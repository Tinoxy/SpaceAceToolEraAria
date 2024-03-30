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
                    if( session.User == null)
                    {
                        continue;
                    }
                    System.Console.WriteLine("Logic");
                    var closest = session.User.NpcClosestTo();
                    System.Console.WriteLine(closest);
                    if(closest == null)
                    {
                        System.Console.WriteLine(closest);
                        System.Console.WriteLine(session.User.Moving);
                        if(session.User.Moving)
                            continue;
                        var random = new Random();
                        var x = random.Next(0, 20000);
                        var y = random.Next(0, 13000);
                        await session.SendPacket("MD|"+x+"|"+y);
                        var timeToEnd = session.User.Position.DistanceTo(new(x , y)) / session.User.Speed * 1000;
                        await session.SendClient($"SHM|{session.User.ID}|{x}|{y}|{timeToEnd}");

                    }
                    if(closest != null && session.User.Target?.ID != closest.ID)
                    {
                        await session.SendPacket(new NpcClicked(closest.ID));
                        session.User.Target = closest;
                        System.Console.WriteLine($"Npc {closest.ID}:{closest.Username} clicked");
                        continue;
                    }
                        await session.SendPacket("SRTA");
                        await Task.Delay(120);
                        await session.SendPacket($"MD|{closest.Position.X}|{closest.Position.Y}");
                        var time = Movement.GetTime(session.User, closest.Position);
                        System.Console.WriteLine($"Time: {time}");
                        if(session.User.Moving)
                        {
                            continue;
                        }
                          var timeToEnd = session.User.Position.DistanceTo(closest.Position) / session.User.Speed * 1000;
                        await session.SendClient($"SHM|{session.User.ID}|{closest.Position.X + 400}|{closest.Position.Y}|{timeToEnd}");
                    }
                catch(Exception e)
                {
                    System.Console.WriteLine(e);
                }
            }
        }
    }
}