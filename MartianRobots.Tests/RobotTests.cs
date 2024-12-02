namespace MartianRobots.Tests
{
    public class RobotTests
    {
        [Fact]
        public void WhenDefaultConstructed_ShouldFaceNorthWithZeroZeroPosition() 
        { 
            //Arrange & Act
            var robot = new Robot();

            //Assert
            Assert.Equal(0, robot.XPosition );
            Assert.Equal(0, robot.YPosition);
            Assert.Equal(Direction.North, robot.Direction);
        }

        [Theory]
        [InlineData(0, 0, Direction.North)]
        [InlineData(1, 3, Direction.East)]
        [InlineData(2, 5, Direction.South)]
        [InlineData(3, 8, Direction.West)]
        public void WhenConstructed_ShouldBeInTheAssignedPostionAndDirection(int xPosition, int yPosition, Direction direction)
        {
            //Arrange & Act
            var robot = new Robot(xPosition: xPosition, yPosition: yPosition, direction: direction);

            //Assert
            Assert.Equal(xPosition, robot.XPosition);
            Assert.Equal(yPosition, robot.YPosition);
            Assert.Equal(direction, robot.Direction);
        }

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