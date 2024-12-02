# Martian Robot Simulation

Welcome to the Martian Robot Simulation! In this application, we've created a model of the surface of Mars as a rectangular grid, where robots can move based on instructions provided by the user. The robots start at specified positions on the grid, facing one of four directions (North, South, East, or West), and follow a set of commands to navigate the Martian terrain.

## How the Program Works:

### 1. Grid Setup
The program starts by taking the upper-right coordinates of the rectangular grid. The lower-left corner is assumed to be at (0, 0).

### 2. Robot Setup
For each robot, the user inputs its starting position (x, y) and its initial orientation (N, S, E, or W).

### 3. Movement Instructions
The user then inputs a series of commands (`L`, `R`, `F`) to move the robot across the grid:
- **L** (Left): The robot turns 90 degrees to the left, without moving.
- **R** (Right): The robot turns 90 degrees to the right, without moving.
- **F** (Forward): The robot moves one grid space in the direction it’s currently facing, maintaining that orientation.

### 4. Tracking Movements
The program processes each movement command in sequence. If a robot moves off the grid, it’s marked as **lost**, and the robot’s last known position is stored to prevent future robots from falling off at the same spot.

### 5. Output
After all commands for a robot are executed, the program outputs the robot’s final position and orientation. If the robot is lost, "LOST" is appended to the output.

### Example:
- For a robot that ends up at position (1, 3) facing East, the output would be: `1 3 E`.
- If the robot falls off the edge, the output would include: `1 3 E LOST`.

The program handles multiple robots in sequence and provides clear feedback for each robot’s journey across the Martian grid. The robot's fate—whether it successfully navigates the grid or gets lost—is determined by the commands provided and the current state of the grid.
