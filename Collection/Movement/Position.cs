using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.Movement
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime BlockMovement { get; set; } = DateTime.MinValue;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public double DistanceTo(Position characterOldPosition) => Math.Sqrt(Math.Pow(characterOldPosition.X - X, 2) + Math.Pow(characterOldPosition.Y - Y, 2));

        public Point GetAsPoint() => new Point(X, Y);
    }
}
