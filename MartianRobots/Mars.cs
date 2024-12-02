using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public class Mars(int width, int height)
    {
        public const int X_LowerLimit = 0;
        public const int Y_LowerLimit = 0;

        public int X_UpperLimit { get; set; } = width;
        public int Y_UpperLimit { get; set; } = height;
    }
}
