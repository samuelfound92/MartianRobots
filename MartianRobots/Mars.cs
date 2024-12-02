using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public class Mars(int x_UpperLimit, int y_UpperLimit)
    {
        /// <summary>
        /// Neither of the upper limits should be above this value
        /// </summary>
        public const int MAX_UPPER_LIMIT = 50;
        public const int LOWER_LIMIT = 0;

        public int X_UpperLimit { get; set; } = ValidateLimit(x_UpperLimit, nameof(x_UpperLimit));
        public int Y_UpperLimit { get; set; } = ValidateLimit(y_UpperLimit, nameof(y_UpperLimit));

        private static int ValidateLimit(int limit, string paramName)
        {
            if (limit < LOWER_LIMIT || limit > MAX_UPPER_LIMIT)
            {
                throw new ArgumentOutOfRangeException(
                    paramName,
                    $"The {paramName} is invalid. {LOWER_LIMIT} - {MAX_UPPER_LIMIT} is the accepted range."
                );
            }

            return limit;
        }
    }
}
