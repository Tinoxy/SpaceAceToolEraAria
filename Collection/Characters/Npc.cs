using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collections.Characters;
using Collections.Movement;
namespace SpaceAceToolEraAria.Collection.Characters
{
    public class Npc : Character // Use the fully qualified name for the base class
    {
        public Npc(string username, Position? position = null) : base(username, position)
        {
        }

        public double DistanceTo(Position position) => Position.DistanceTo(position); 
    }
}