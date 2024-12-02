using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Tests
{
    public class MarsTests
    {
        [Theory]
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
        public void WhenConstructingWithTooLargeUpperLimits_ShouldThrowError(int x_UpperLimit, int y_UpperLimit)
        {
            //Arrange
            var constructMars = () => new Mars(x_UpperLimit, y_UpperLimit);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(constructMars);
        }
    }
}
