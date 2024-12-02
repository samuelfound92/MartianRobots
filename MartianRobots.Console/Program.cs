using System;
using MartianRobots;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Martian Robots!");
        Console.WriteLine("Enter Mars grid upper limits (e.g., 5 5 for a 5x5 grid):");

        // Read and parse grid upper limits
        var limits = Console.ReadLine()?.Split(' ');
        if (limits == null || limits.Length != 2 ||
            !int.TryParse(limits[0], out int xUpper) ||
            !int.TryParse(limits[1], out int yUpper))
        {
            Console.WriteLine("Invalid input. Exiting.");
            return;
        }

        try
        {
            Mars mars = new Mars(xUpper, yUpper);

            Console.WriteLine("Mars grid initialized.");
            Console.WriteLine("Commands: Forward (F), Turn Left (L), Turn Right (R), Exit (E)");

            bool running = true;

            while (running)
            {
                Console.WriteLine($"Current Robot Position: ({mars.Robot.XPosition}, {mars.Robot.YPosition}), Facing: {mars.Robot.Direction}");
                Console.WriteLine("Enter command string (e.g., FFLR) or E to exit:");

                var commandsInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(commandsInput) || commandsInput.Equals("E", StringComparison.CurrentCultureIgnoreCase))
                {
                    running = false;
                    break;
                }

                foreach (var commandChar in commandsInput.ToUpper())
                {
                    if (!TryParseCommand(commandChar, out RobotCommand command))
                    {
                        Console.WriteLine($"Invalid command '{commandChar}'. Skipping.");
                        continue;
                    }

                    var outcome = mars.ExecuteCommand(command);

                    // Handle different outcomes
                    switch (outcome)
                    {
                        case RobotCommandOutcome.Succeeded:
                            Console.WriteLine($"Command '{command}' executed successfully.");
                            break;
                        case RobotCommandOutcome.Rejected:
                            Console.WriteLine($"Command '{command}' was rejected (robot at grave or invalid move).");
                            break;
                        case RobotCommandOutcome.RobotHasDied:
                            Console.WriteLine("The robot has fallen off the grid and was reset to the starting position.");
                            break;
                    }
                }
            }

            Console.WriteLine("Thank you for using Martian Robots!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static bool TryParseCommand(char input, out RobotCommand command)
    {
        command = input switch
        {
            'F' => RobotCommand.Forward,
            'L' => RobotCommand.TurnLeft,
            'R' => RobotCommand.TurnRight,
            _ => RobotCommand.Forward // Default; we validate invalid commands separately
        };
        return input is 'F' or 'L' or 'R';
    }
}
