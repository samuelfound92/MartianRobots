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
        public void WhenConstructed_ShouldHaveCorrectDimensions(int x_UpperLimit, int y_UpperLimit) 
        {
            //Arrange
            var mars = new Mars(x_UpperLimit, y_UpperLimit);

            //Assert
            Assert.Equal(x_UpperLimit, mars.X_UpperLimit);
            Assert.Equal(y_UpperLimit, mars.Y_UpperLimit);
        }
    }
}
