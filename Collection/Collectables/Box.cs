using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collections.Movement;

namespace SpaceAceToolEraAria.Collection.Collectables
{
    public class Box
    {
        public string Hash { get; set;}
        public Position Position { get; set; }
        public Box(string hash,Position position)
        {
            Hash = hash;
            Position = position;
        }
    }
}