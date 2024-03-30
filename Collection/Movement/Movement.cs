using Collections.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.Movement
{
    public class Movement
    {
        public static void Move(Character character, Position destination, int time = -1)
        {
            var movementTime = GetTime(character, destination, time);
            if(movementTime <= 0)
                return;
            character.MovementTime = movementTime;
            character.MovementStartTime = DateTime.UtcNow;
            character.Moving = true;
        }

        public static int GetTime(Character character, Position destination, int duration = -1)
        {
            try
            {
                character.OldPosition = ActualPosition(character);
                var destinationPosition = destination;
                character.Destination = destinationPosition;
                character.Direction = new Position(destinationPosition.X - character.OldPosition.X, destinationPosition.Y - character.OldPosition.Y);
                var distance = destinationPosition.DistanceTo(character.OldPosition);
                double time = 0;
                if (duration == -1)
                {
                    //we will make it a bit slower there to ensure we are really moving fast
                    time = Math.Round(distance / character.Speed * 1000);
                }
                else
                {
                    time = duration;
                }
                return (int)time;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return -1;
        }
        public static Position ActualPosition(Character character)
        {
            // If the character is not moving, simply return the current position
            if (!character.Moving)
            {
                return character.Position;
            }
            // Calculate the time elapsed since movement started
            double timeElapsed = (DateTime.UtcNow - character.MovementStartTime).TotalMilliseconds;
            // If the movement has completed
            if (timeElapsed >= character.MovementTime)
            {
                character.Moving = false;
                character.Destination.BlockMovement = character.Position.BlockMovement;
                character.Position = character.Destination; // Ensure the character is exactly at the destination
                return character.Destination;
            }
            // Calculate the progress ratio of the movement
            double progress = timeElapsed / character.MovementTime;
            // Calculate the new position based on the progress
            double newX = character.OldPosition.X + (character.Direction.X * progress);
            double newY = character.OldPosition.Y + (character.Direction.Y * progress);
            // Set the character's position to the newly calculated position
            var position = new Position((int)newX, (int)newY)
            {
                BlockMovement = character.Position.BlockMovement
            };
            character.Position = position;
            return position;
        }

    }
}
