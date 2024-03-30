using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collections.Movement;
using SpaceAceToolEraAria.Connection;
using SpaceAceToolEraAria.Tick;

namespace Collections.Characters
{
    public abstract class Character : ITick
    {
        public string Username { get; set; }
        public Position OldPosition;
        public Position Destination;
        public Position Direction;
        public bool Moving = false;
        public int Speed = 0;
        public DateTime LastAttacked = DateTime.MinValue;
        public event EventHandler<ITick> OnRemove;

        public Position Position { get; set; }
        public DateTime MovementStartTime { get; set; } 
        public int MovementTime { get; set; }
        public string ID { get; set; }
        public int FactionID { get; set; }
        public virtual int MaxHP { get; set; }
        public virtual int MaxShd { get; set; }
        public virtual int Hp { get; set; }
        public virtual int Shd { get; set; }
        public int TempId { get; set; }
        public int NanoHp { get; set; }
        public int MaxNano { get; set; }
        public bool Cloacked { get; set; }
        public int Level { get; set; }
        public int MapId { get; set; }
        public Guid Id { get; set; }
        public bool Ticking { get;set;}

        protected Character(string username, Position? position = null)
        {
            position ??= new(0 , 0);
            ID = "";
            Username = username;
            MovementStartTime = DateTime.MinValue;
            OldPosition = position;
            Destination = position;
            Direction = position;
            Position = position;
            MovementTime = 0;
            Id = Guid.NewGuid();
            Session.TickManager.AddTick(this);
        }
        public void Update(int hitPoints, int maxHp, int shield, int maxShd)
        {
            Hp = hitPoints;
            MaxHP = maxHp;
            Shd = shield;
            MaxShd = maxShd;
        }

        public void Tick()
        {
            Movement.Movement.ActualPosition(this);
        }
        public void Remove()
        {
            OnRemove?.Invoke(this, this);
        }
    }
}
