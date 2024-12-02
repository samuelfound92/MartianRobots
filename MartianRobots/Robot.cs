using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public class Robot(int xPosition = 0, int yPosition = 0, Direction direction = Direction.North)
    {
        public int XPosition { get; private set; } = xPosition;
        public int YPosition { get; private set; } = yPosition;
        public Direction Direction { get; private set; } = direction;

        public Direction TurnRight()
        {
            return Rotate(1); // Rotate clockwise
        }

        public Direction TurnLeft()
        {
            return Rotate(-1); // Rotate counterclockwise
        }

        private Direction Rotate(int step)
        {
            int directionCount = Enum.GetValues(typeof(Direction)).Length; // Get the total number of directions
            Direction = (Direction)(((int)Direction + step + directionCount) % directionCount); // Ensure circular rotation
            return Direction;
        }

        public (int xPosition, int yPosition) MoveForward()
        {
            // Update position based on current direction
            (XPosition, YPosition) = GetNextPosition();
            return (XPosition, YPosition); // Return updated position
        }


        /// <summary>
        /// Calculate the next position based on the current direction
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public (int xPosition, int yPosition) GetNextPosition()
        {
            return Direction switch
            {
                Direction.North => (XPosition, YPosition + 1), // Tile above
                Direction.East => (XPosition + 1, YPosition), // Tile to the right
                Direction.South => (XPosition, YPosition - 1), // Tile below
                Direction.West => (XPosition - 1, YPosition), // Tile to the left
                _ => throw new NotImplementedException($"Direction {Direction} is not supported")
            };
        }
    }

    public enum Direction
    {
        North,
        East,
        South,
        West,
    }
}
