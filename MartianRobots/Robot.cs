using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public class Robot
    {
        public int XPosition { get; private set; }
        public int YPosition { get; private set; }
        public Direction Direction { get; private set; }

        public Robot(int xPosition = 0, int yPosition = 0, Direction direction = Direction.North)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Direction = direction;
        }

        public Direction TurnRight()
        {
            if (Direction == Direction.West)
            {
                Direction = Direction.North;
            }
            else
            {
                Direction++;
            }

            return Direction;
        }

        public Direction TurnLeft()
        {
            if (Direction == Direction.North)
            {
                Direction = Direction.West;
            }
            else
            {
                Direction--;
            }

            return Direction;
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
