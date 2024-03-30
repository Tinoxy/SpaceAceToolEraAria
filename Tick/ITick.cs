using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceAceToolEraAria.Tick
{
    public interface ITick
    {
        void Tick();
        Guid Id { get; set; }
        event EventHandler<ITick> OnRemove;
        bool Ticking { get; set; }
    }
}