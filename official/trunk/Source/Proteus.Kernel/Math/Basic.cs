using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Math
{
    public static class Basic
    {
        /// <summary>simple PI - no discussion</summary>
        public static float     PI = (float)System.Math.PI;

        public static float     FloatEpsilon    = float.MinValue;
        public static double    DoubleEpsilon   = double.MinValue;

        /// <summary>
        /// calculates the square root of a certain value
        /// </summary>
        /// <param name="value">to calc sqrt from</param>
        /// <returns>sqrt of value</returns>
        public static float Sqrt(float value)
        {
            return (float)System.Math.Sqrt(value);
        }

        /// <summary>
        /// Calculates the inverse square root of a certain value.
        /// </summary>
        /// <param name="x">Value to calc the inverse sqrt from.</param>
        /// <returns>The inverse sqrt.</returns>
        public unsafe static float InvSqrt(float x)
        {
            return 1.0f / Sqrt(x);
        }

        /// <summary>
        /// calculates the absolute value
        /// </summary>
        /// <param name="value">to calc absolute value from</param>
        /// <returns>absolute value</returns>
        public static float Abs(float value)
        {
            return System.Math.Abs(value);
        }

        /// <summary>
        /// Returns the natural (base e) logarithm of a specified number.
        /// </summary>
        /// <param name="a">The number to calculate the logarithm from.</param>
        /// <returns>The natural (base e) logarithm of a specified number.</returns>
        public static float Log(double a)
        {
            return (float)System.Math.Log(a);
        }

        /// <summary>
        /// Returns the logarithm of a specified number in a specified base.
        /// </summary>
        /// <param name="a">The number to calculate the logarithm from.</param>
        /// <param name="newBase">The base to use.</param>
        /// <returns>The logarithm of a specified number in a specified base.</returns>
        public static float Log(double a, double newBase)
        {
            return (float)System.Math.Log(a, newBase);
        }

        /// <summary>
        /// Returns the base 10 logarithm of a specified number.
        /// </summary>
        /// <param name="a">The number to calculate the logarithm from.</param>
        /// <returns>The base 10 logarithm of a specified number.</returns>
        public static float Log10(double a)
        {
            return (float)System.Math.Log10(a);
        }

        /// <summary>
        /// Returns e raised to the specified power.
        /// </summary>
        /// <param name="power">The power to raise e.</param>
        /// <returns>E raised to the specified power.</returns>
        public static float Exp(double power)
        {
            return (float)System.Math.Exp(power);
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="a">A number to be raised to a power.</param>
        /// <param name="power">A number that specifies a power.</param>
        /// <returns>A specified number raised to the specified power.</returns>
        public static float Pow(double a, double power)
        {
            return (float)System.Math.Pow(a, power);
        }

        /// <summary>
        /// Clamps the value to the given range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value!</returns>
        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        /// <summary>
        /// Clamps the value to the given range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value!</returns>
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        public static bool Equals(float a, float b)
        {
            if (a > b - FloatEpsilon &&
                 a < b + FloatEpsilon)
            {
                return true;
            }

            return false;
        }

        public static bool Equals(double a, double b)
        {
            if (a > b - DoubleEpsilon &&
                a < b + DoubleEpsilon)
            {
                return true;
            }

            return false;
        }

	    public static float DegToRad( float angle )
	    {
		    return (angle * (PI / 180.0f));
	    }

	    public static float RadToDeg( float angle )
	    {
		    return (angle * (180.0f / PI));
	    }

        public static double DegToRad(double angle)
        {
            return (angle * (System.Math.PI / 180.0));
        }

        public static double RadToDeg(double angle)
        {
            return (angle * (180.0 / System.Math.PI ));
        }
    }
}
