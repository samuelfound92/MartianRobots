namespace MartianRobots.Tests
{
    public class RobotTests
    {
        [Fact]
        public void WhenTurning_ShouldGiveCorrectDirecton()
        {
            //Arrange
            var robot = new Robot();

            //Act
            robot.TurnRight();

            //Assert
            Assert.Equal(Direction.East, robot.Direction);
        }
    }
}