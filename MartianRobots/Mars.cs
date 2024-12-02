using System;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobots
{
    public class Mars(int x_UpperLimit, int y_UpperLimit)
    {
        public const int MAX_UPPER_LIMIT = 50;
        public const int LOWER_LIMIT = 0;

        public Robot Robot { get; private set; } = new Robot();
        public List<(int x, int y)> RobotGraves { get; private set; } = [];

        public int X_UpperLimit { get; private set; } = ValidateLimit(x_UpperLimit, nameof(x_UpperLimit));
        public int Y_UpperLimit { get; private set; } = ValidateLimit(y_UpperLimit, nameof(y_UpperLimit));

        public Robot BuildRobot(int xPosition, int yPosition, Direction direction) 
        { 
            Robot = new Robot(xPosition, yPosition, direction);
            return Robot;
        }

        private static int ValidateLimit(int limit, string paramName)
        {
            if (limit < LOWER_LIMIT || limit > MAX_UPPER_LIMIT)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    $"The {paramName} is invalid. {LOWER_LIMIT} - {MAX_UPPER_LIMIT} is the accepted range."
                );
            }
            return limit;
        }

        public bool CheckCoordinatesAreValid(int xCoordinate, int yCoordinate)
        {
            return xCoordinate >= LOWER_LIMIT && xCoordinate <= X_UpperLimit &&
                   yCoordinate >= LOWER_LIMIT && yCoordinate <= Y_UpperLimit;
        }

        public RobotCommandOutcome ExecuteCommand(RobotCommand command)
        {
            return command switch
            {
                RobotCommand.Forward => MoveRobotForward(),
                RobotCommand.TurnLeft => PerformRotation(Robot.TurnLeft),
                RobotCommand.TurnRight => PerformRotation(Robot.TurnRight),
                _ => throw new NotImplementedException($"Command {command} is not recognized.")
            };
        }

        private static RobotCommandOutcome PerformRotation(Func<Direction> rotationMethod)
        {
            rotationMethod();
            return RobotCommandOutcome.Succeeded;
        }

        private RobotCommandOutcome MoveRobotForward()
        {
            if (!CanMoveForward) return RobotCommandOutcome.Rejected;

            if (IsNextPositionSafe)
            {
                Robot.MoveForward();
                return RobotCommandOutcome.Succeeded;
            }
            else
            {
                RecordRobotDeath();
                return RobotCommandOutcome.RobotHasDied;
            }
        }

        private void RecordRobotDeath()
        {
            RobotGraves.Add((Robot.XPosition, Robot.YPosition));
        }

        private bool CanMoveForward => !IsOnGraveSite || IsNextPositionSafe;

        private bool IsNextPositionSafe
        {
            get
            {
                var (xPosition, yPosition) = Robot.GetNextPosition();
                return CheckCoordinatesAreValid(xPosition, yPosition);
            }
        }

        private bool IsOnGraveSite => RobotGraves.Contains((Robot.XPosition, Robot.YPosition));
    }

    public enum RobotCommand
    {
        Forward,
        TurnLeft,
        TurnRight,
    }

    public enum RobotCommandOutcome
    {
        Succeeded,
        Rejected,
        RobotHasDied,
    }
}
