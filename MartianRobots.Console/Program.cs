using System;
using System.Collections.Generic;
using MartianRobots;

namespace MartianRobotsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Input for the grid size
            Console.WriteLine("Enter the upper-right coordinates of the rectangular grid (e.g., 5 3):");
            var gridInput = Console.ReadLine()?.Split(' ');

            if (!IsValidGridInput(gridInput, out int xUpperLimit, out int yUpperLimit))
            {
                Console.WriteLine("Invalid grid size input. Exiting.");
                return;
            }

            Mars mars = new(xUpperLimit, yUpperLimit);

            // Step 2: Input for robot positions and instructions
            Console.WriteLine("Enter robot positions and instructions one at a time:");

            string robotInput;
            while (!string.IsNullOrWhiteSpace(robotInput = Console.ReadLine()))
            {
                // Process robot position
                var positionParts = robotInput.Split(' ');
                if (!IsValidPositionInput(positionParts, mars, out int xPosition, out int yPosition, out Direction direction))
                {
                    Console.WriteLine("Invalid robot position input. Skipping...");
                    continue;
                }

                // Build the robot on the Mars grid
                mars.BuildRobot(xPosition, yPosition, direction);

                // Step 3: Input for movement instructions
                string instructions = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(instructions) || !IsValidInstructionInput(instructions))
                {
                    Console.WriteLine("Invalid robot instructions. Skipping...");
                    continue;
                }

                // Process the robot commands
                string result = ProcessRobotCommands(mars, instructions);
                Console.WriteLine(result);
            }

            Console.WriteLine("Processing complete.");
        }

        // Validates grid input (x, y)
        private static bool IsValidGridInput(string[] gridInput, out int x, out int y)
        {
            x = y = 0;

            if (gridInput == null || gridInput.Length != 2 ||
                !int.TryParse(gridInput[0], out x) || !int.TryParse(gridInput[1], out y))
                return false;

            return x >= 0 && x <= Mars.MAX_UPPER_LIMIT && y >= 0 && y <= Mars.MAX_UPPER_LIMIT;
        }

        // Validates robot's initial position (x, y, direction)
        private static bool IsValidPositionInput(string[] positionInput, Mars mars, out int x, out int y, out Direction direction)
        {
            x = y = 0;
            direction = Direction.North;

            if (positionInput.Length != 3 ||
                !int.TryParse(positionInput[0], out x) ||
                !int.TryParse(positionInput[1], out y) ||
                !TryParseCardinalDirection(positionInput[2], out direction) ||
                !mars.CheckCoordinatesAreValid(x, y))
                return false;

            return true;
        }

        // Attempts to parse a cardinal direction ('N', 'E', 'S', 'W')
        private static bool TryParseCardinalDirection(string input, out Direction direction)
        {
            direction = Direction.North; // Default value

            if (string.IsNullOrEmpty(input))
                return false;

            switch (input.ToUpper())
            {
                case "N":
                    direction = Direction.North;
                    return true;
                case "E":
                    direction = Direction.East;
                    return true;
                case "S":
                    direction = Direction.South;
                    return true;
                case "W":
                    direction = Direction.West;
                    return true;
                default:
                    return false;
            }
        }

        // Validates robot movement instructions (only 'L', 'R', 'F')
        private static bool IsValidInstructionInput(string instructions)
        {
            if (string.IsNullOrWhiteSpace(instructions) || instructions.Length > 100)
                return false;

            foreach (char c in instructions)
            {
                if (!"LRF".Contains(c))
                    return false;
            }

            return true;
        }

        // Processes the movement instructions of the robot
        private static string ProcessRobotCommands(Mars mars, string instructions)
        {
            foreach (char commandChar in instructions)
            {
                if (!TryParseCommand(commandChar, out RobotCommand command))
                {
                    Console.WriteLine($"Invalid command '{commandChar}'. Ignoring.");
                    continue;
                }

                var outcome = mars.ExecuteCommand(command);
                if (outcome == RobotCommandOutcome.RobotHasDied)
                {
                    return $"{mars.Robot.XPosition} {mars.Robot.YPosition} {mars.Robot.Direction} LOST";
                }
            }

            return $"{mars.Robot.XPosition} {mars.Robot.YPosition} {mars.Robot.Direction}";
        }

        // Attempts to parse robot movement commands ('F', 'L', 'R')
        private static bool TryParseCommand(char input, out RobotCommand command)
        {
            command = input switch
            {
                'F' => RobotCommand.Forward,
                'L' => RobotCommand.TurnLeft,
                'R' => RobotCommand.TurnRight,
                _ => RobotCommand.Forward // Default case
            };

            return input is 'F' or 'L' or 'R';
        }
    }
}
