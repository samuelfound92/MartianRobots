namespace MartianRobots.Tests
{
    public class RobotTests
    {
        [Theory]
        [InlineData(Direction.North, 0, Direction.North)]
        [InlineData(Direction.North, 4, Direction.North)]
        [InlineData(Direction.South, 1, Direction.West)]
        public void WhenTurningRight_ShouldGiveCorrectDirecton(Direction startingDirection, int numberOfRightTurns, Direction expectedEndDirection)
        {
            //Arrange
            var robot = new Robot(direction: startingDirection);

            //Act
            for (int i = 0; i < numberOfRightTurns; i++)
            {
                robot.TurnRight();
            }

            //Assert
            Assert.Equal(expectedEndDirection, robot.Direction);
        }

        [Theory]
        [InlineData(Direction.East, 0, Direction.East)]
        [InlineData(Direction.East, 4, Direction.East)]
        [InlineData(Direction.South, 1, Direction.East)]
        public void WhenTurningLeft_ShouldGiveCorrectDirecton(Direction startingDirection, int numberOfRightTurns, Direction expectedEndDirection)
        {
            //Arrange
            var robot = new Robot(direction: startingDirection);

            //Act
            for (int i = 0; i < numberOfRightTurns; i++)
            {
                robot.TurnLeft();
            }

            //Assert
            Assert.Equal(expectedEndDirection, robot.Direction);
        }
    }
}