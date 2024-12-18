﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Tests
{
    public class MarsTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(20, 30)]
        [InlineData(Mars.MAX_UPPER_LIMIT, Mars.MAX_UPPER_LIMIT)]
        public void WhenConstructed_ShouldHaveCorrectDimensions(int x_UpperLimit, int y_UpperLimit) 
        {
            //Arrange
            var mars = new Mars(x_UpperLimit, y_UpperLimit);

            //Assert
            Assert.Equal(x_UpperLimit, mars.X_UpperLimit);
            Assert.Equal(y_UpperLimit, mars.Y_UpperLimit);
        }

        [Theory]
        [InlineData(Mars.MAX_UPPER_LIMIT, Mars.MAX_UPPER_LIMIT + 1)]
        [InlineData(Mars.MAX_UPPER_LIMIT + 1, Mars.MAX_UPPER_LIMIT)]
        [InlineData(Mars.LOWER_LIMIT, Mars.LOWER_LIMIT - 1)]
        [InlineData(Mars.LOWER_LIMIT - 1, Mars.LOWER_LIMIT)]
        public void WhenConstructingWithTooLargeUpperLimits_ShouldThrowError(int x_UpperLimit, int y_UpperLimit)
        {
            //Arrange
            var constructMars = () => new Mars(x_UpperLimit, y_UpperLimit);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(constructMars);
        }

        [Theory]
        [InlineData(11, 10)]
        [InlineData(-1, 2)]
        public void WhenCheckingAnOutOfRangeCoordinate_ShouldBeFlaggedAsSuch(int x, int y) 
        {
            //Arrange
            var mars = new Mars(10, 10);

            //Act
            var isValid = mars.CheckCoordinatesAreValid(x, y);

            //Assert
            Assert.False(isValid);
        }

        [Fact]
        public void WhenRobotGoesOutOfBounds_ShouldBeGivenAGraveSite() 
        {
            //Arrange
            var mars = new Mars(0, 0);

            //Act
            var outcome = mars.ExecuteCommand(RobotCommand.Forward);

            //Assert
            Assert.Single(mars.RobotGraves);
            Assert.Equal(RobotCommandOutcome.RobotHasDied, outcome);
        }
    }
}
