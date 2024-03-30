using System.Collections.Concurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collections.Characters;
using Collections.Movement;
using SpaceAceToolEraAria.Collection.Characters;

namespace SpaceAceToolEraAria
{
    public class User : Character
    {
        public User(string username, Position? position = null) : base(username, position)
        {
        }

        public Npc? Target;

        private ConcurrentDictionary<string , Npc> Npcs = new();

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
    }
}