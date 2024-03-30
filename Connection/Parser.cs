using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceAceToolEraAria.Connection.Coomands;
using SpaceAceToolEraAria.Connection.Coomands.Read;

namespace SpaceAceToolEraAria.Connection
{
    public static class Parser
    {
        private readonly static Dictionary<string, IPacket> _commands = new()
        {
            { CharacterInitialize.ID, new CharacterInitialize() },
            { HeroInitialize.Id , new HeroInitialize() },
            { HeroMoved.ID , new HeroMoved() },
            { RemoveCharacter.ID , new RemoveCharacter() },
            { NpcClicked.ID , new NpcClicked() },
            {CharacterMoved.ID , new CharacterMoved()}

        };   
        public static void Parse(Session session ,string message)
        {
            var command = message.Split('|')[0];
            if(_commands.TryGetValue(command , out var packet))
                packet.Read(session , message);
            
        }
    }
}