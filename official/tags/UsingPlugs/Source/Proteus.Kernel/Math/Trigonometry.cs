using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Math
{
    public static class Trigonometry
    {
        /// <summary>
        /// cosinus function
        /// </summary>
        /// <param name="angle">angle in radians</param>
        /// <returns>the cos of the specified angle</returns>
        public static float Cos(float angle)
        {
            return (float)System.Math.Cos(angle);
        }

        /// <summary>
        /// Returns the angle whose cosine is the specified number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>The angle whose cosine is the specified number.</returns>
        public static float Acos(float number)
        {
            return (float)System.Math.Acos(number);
        }

        /// <summary>
        /// sinus function
        /// </summary>
        /// <param name="angle">angle in radians</param>
        /// <returns>the sin of the specified angle</returns>
        public static float Sin(float angle)
        {
            return (float)System.Math.Sin(angle);
        }

        /// <summary>
        /// co-tangens function (cos/sin)
        /// </summary>
        /// <param name="angle">angle in radians</param>
        /// <returns>co-tangens of specified angle</returns>
        public static float Cot(float angle)
        {
            return Cos(angle) / Sin(angle);
        }
    }
}
