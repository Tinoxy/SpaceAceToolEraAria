using System.Collections.Concurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collections.Characters;
using Collections.Movement;
using SpaceAceToolEraAria.Collection.Characters;
using SpaceAceToolEraAria.Collection.Collectables;

namespace SpaceAceToolEraAria
{
    public class User : Character
    {
        public User(string username, Position? position = null) : base(username, position)
        {
        }

        public Npc? Target;
        public Box? Box;

        private ConcurrentDictionary<string , Npc> Npcs = new();
        private ConcurrentDictionary<string , Box> Boxes = new();

        public void AddNpc(Npc npc)
        {
            System.Console.WriteLine($"Adding NPC {npc.Username}");
            Npcs.TryAdd(npc.ID, npc);
        }
        public void RemoveNpc(string id)
        {
            if(Target?.ID == id)
            {
                Target = null;
            }
            System.Console.WriteLine($"Removing NPC {id}");
            Npcs.TryRemove(id, out var npcToDispose);
            npcToDispose?.Remove();
        }

        internal Npc? GetNpc(string id)
        {
            Npcs.TryGetValue(id, out var npc);
            return npc;
        }
        public bool AnyNpc() => Npcs.Any();

        public Npc NpcClosestTo()
        {
            var closest = Npcs.Values.Where(x => x.Username.ToLower().Contains("streuner") || x.Username.ToLower().Contains("lordakia")).OrderBy(npc => npc.DistanceTo(Position)).FirstOrDefault();
            return closest;
        }

        public void AddBox(Box box)
        {
            System.Console.WriteLine($"Adding Box {box.Hash}");
            Boxes.TryAdd(box.Hash, box);
        }
        public void RemoveBox(string id)
        {
            if(Box?.Hash == id)
            {
                Box = null;
            }
            System.Console.WriteLine($"Removing Box {id}");
            Boxes.TryRemove(id, out var boxToDispose);
        }

        public Box? GetClosestBox()
        {
            var closest = Boxes.Values.OrderBy(box => box.Position.DistanceTo(Position)).FirstOrDefault();
            return closest;
        }
    }
}