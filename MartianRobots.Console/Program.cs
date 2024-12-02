using System;
using System.Collections.Generic;
using MartianRobots;

namespace MartianRobotsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the upper-right coordinates of the rectangular grid (e.g., 5 3):");
            var gridInput = Console.ReadLine()?.Split(' ');

            if (!IsValidGridInput(gridInput, out int xUpperLimit, out int yUpperLimit))
            {
                Console.WriteLine("Invalid grid size input. Exiting.");
                return;
            }

            Mars mars = new(xUpperLimit, yUpperLimit);

            Console.WriteLine("Enter robot positions and instructions (type 'END' to finish input):");

            string robotInput;
            while (!string.IsNullOrWhiteSpace(robotInput = Console.ReadLine()) && robotInput.ToUpper() != "END")
            {
                var positionParts = robotInput.Split(' ');
                if (!IsValidPositionInput(positionParts, mars, out int xPosition, out int yPosition, out Direction direction))
                {
                    Console.WriteLine("Invalid robot position input. Skipping...");
                    continue;
                }

                string instructions = Console.ReadLine();
                if (!IsValidInstructionInput(instructions))
                {
                    Console.WriteLine("Invalid robot instructions. Skipping...");
                    continue;
                }

                mars.BuildRobot(xPosition, yPosition, direction);
                string result = ProcessRobotCommands(mars, instructions);
                Console.WriteLine(result);
            }

            Console.WriteLine("Processing complete.");
        }

        private static bool IsValidGridInput(string[] gridInput, out int x, out int y)
        {
            x = y = 0;

            if (gridInput == null || gridInput.Length != 2 ||
                !int.TryParse(gridInput[0], out x) || !int.TryParse(gridInput[1], out y))
                return false;

            return x >= 0 && x <= Mars.MAX_UPPER_LIMIT && y >= 0 && y <= Mars.MAX_UPPER_LIMIT;
        }

        private static bool IsValidPositionInput(string[] positionInput, Mars mars, out int x, out int y, out Direction direction)
        {
            x = y = 0;
            direction = Direction.North;

            if (positionInput.Length != 3 ||
                !int.TryParse(positionInput[0], out x) ||
                !int.TryParse(positionInput[1], out y) ||
                !Enum.TryParse(positionInput[2], true, out direction) ||
                !mars.CheckCoordinatesAreValid(x, y))
                return false;

            return true;
        }

        private static bool IsValidInstructionInput(string instructions)
        {
            if (string.IsNullOrWhiteSpace(instructions) || instructions.Length > 100)
                return false;

            return true;
        }

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
