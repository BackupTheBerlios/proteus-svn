using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace Proteus.Kernel.Math
{
    public class Vector2Converter : ExpandableObjectConverter
    {
        /// <summary>
        /// Converts the given value object to the specified type, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="culture">A CultureInfo object. If a null reference (Nothing in Visual Basic) is passed, the current culture is assumed.</param>
        /// <param name="value">The Object to convert.</param>
        /// <param name="destinationType"></param>
        /// <returns>The Type to convert the value parameter to.</returns>
        public override object ConvertTo(ITypeDescriptorContext context,
          CultureInfo culture, object value, Type destinationType)
        {

            if (destinationType == typeof(string) &&
              value is Vector2)
            {
                Vector2 pt = (Vector2)value;
                return pt.X.ToString(culture) + "; " + pt.Y.ToString(culture);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Converts the given value to the type of this converter.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context. </param>
        /// <param name="culture">The CultureInfo to use as the current culture. </param>
        /// <param name="value">The Object to convert. </param>
        /// <returns>An Object that represents the converted value.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                try
                {
                    string str = (string)value;
                    string[] elements = str.Split(';');
                    return new Vector2(float.Parse(elements[0], culture), float.Parse(elements[1], culture));
                }
                catch
                {
                    throw new ArgumentException("Invalid value: " + value.ToString());
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Returns whether this converter can convert an object of the given type to the type of this converter, 
        /// using the specified context.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context. </param>
        /// <param name="sourceType">A Type that represents the type you want to convert from. </param>
        /// <returns>True if this converter can perform the conversion; otherwise, false.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }
    }

    [System.ComponentModel.TypeConverter(typeof(Vector2Converter))]
    public struct Vector2
    {
        //---------------------------------------------------------------
        #region Variables
        //---------------------------------------------------------------
        private float x, y;
        //---------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------

        //---------------------------------------------------------------
        #region Properties
        //---------------------------------------------------------------
        /// <summary>X element of the vector.</summary>
        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        /// <summary>Y element of the vector.</summary>
        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        /// <summary>
        /// returns the a vector initialised with 0.0f
        /// </summary>
        public static Vector2 Zero
        {
            get
            {
                return new Vector2();
            }
        }

        /// <summary>
        /// returns a vector initialised with 1.0f
        /// </summary>
        public static Vector2 One
        {
            get
            {
                return new Vector2(1.0f, 1.0f);
            }
        }
        //---------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------

        //---------------------------------------------------------------
        #region Initialisation
        //---------------------------------------------------------------
        /// <summary>
        /// constructor for filling all elements
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>		
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Creates a vector from a byte array.
        /// </summary>
        /// <param name="bytes">Array of at least 8 byte elements.</param>
        /// <returns>Created vector.</returns>
        public static Vector2 From(byte[] bytes)
        {
            return From(bytes, 0);
        }

        /// <summary>
        /// Creates a vector from a byte array.
        /// </summary>
        /// <param name="bytes">Array of at least 8 byte elements.</param>
        /// <param name="offset">The start offset in the buffer.</param>
        /// <returns>Created vector.</returns>
        public unsafe static Vector2 From(byte[] bytes, int offset)
        {
            //System.Diagnostics.Debug.Assert(bytes.Length >= (8 + offset));
            fixed (byte* p = &bytes[offset])
            {
                float* floatPtr = (float*)(p);
                return new Vector2(floatPtr[0], floatPtr[1]);
            }
        }
        //---------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------

        //---------------------------------------------------------------
        #region Methods
        //---------------------------------------------------------------
        /// <summary>
        /// new hashcode function
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        /// <summary>
        /// new equals function
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Vector2 vec = (Vector2)obj;
            return vec.x == x && vec.y == y;
        }

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <returns>The string representation of the object.</returns>
        public override string ToString()
        {
            return "X: " + x + " Y: " + y;
        }

        /// <summary>
        /// calc unit vector from a certain Vector
        /// </summary>
        /// <param name="a">vector to calc unit vector from</param>
        /// <returns>unit vector</returns>
        public static Vector2 Unit(Vector2 a)
        {
            return a / a.Length();
        }

        /// <summary>
        /// Calculates the orthogonal vector that points to the right.
        /// </summary>
        /// <param name="a">Vector to calculate orthogonal vector from.</param>
        /// <returns>The orthogonal vector.</returns>
        public static Vector2 Ortho(Vector2 a)
        {
            return new Vector2(a.Y, -a.X);
        }

        /// <summary>
        /// normalizes the current instance
        /// </summary>
        public void Normalize()
        {
            this /= this.Length();
        }

        /// <summary>
        /// Calculates the length of the current vector.
        /// </summary>
        /// <returns>Length of the current vector.</returns>
        public float Length()
        {
            return Basic.Sqrt(this * this);
        }

        /// <summary>
        /// Calculates the squared length of the current vector.
        /// </summary>
        /// <returns>The squared length of the current vector.</returns>
        public float LengthSquared()
        {
            return this * this;
        }

        /// <summary>
        /// creates a vector from an array
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vector2 FromArray(float[] vec)
        {
            System.Diagnostics.Debug.Assert(vec.Length == 2, "Array.Length != 2");
            return new Vector2(vec[0], vec[1]);
        }

        /// <summary>
        /// linear interpolation between to vectors
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <param name="time">interpolation time [0..1]</param>
        /// <returns>interpolated vector</returns>
        public static Vector2 Lerp(Vector2 a, Vector2 b, float time)
        {
            return new Vector2(a.x + (b.x - a.x) * time,
                a.y + (b.y - a.y) * time);
        }
        //---------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------

        //---------------------------------------------------------------
        #region Operations
        //---------------------------------------------------------------
        /// <summary>
        /// add two vectors
        /// </summary>
        /// <param name="a">vector a</param>
        /// <param name="b">vector b</param>
        /// <returns>sum of the two vedctors</returns>
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        /// <summary>
        /// subtracts Vector be from Vector a
        /// </summary>
        /// <param name="a">vector to subtract from</param>
        /// <param name="b">vector to subtract</param>
        /// <returns>result of subtraction</returns>
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        /// <summary>
        /// Negate vector
        /// </summary>
        /// <param name="a">vector to negate</param>
        /// <returns>negative vector</returns>
        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.x, -a.y);
        }

        /// <summary>
        /// divide vector
        /// </summary>
        /// <param name="vec">vector to divide</param>
        /// <param name="divisor">divisor to divide vector with</param>
        /// <returns>vector/divisor</returns>
        public static Vector2 operator /(Vector2 vec, float divisor)
        {
            return new Vector2(vec.x / divisor, vec.y / divisor);
        }

        /// <summary>
        /// scalar product
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>scalar product</returns>
        public static float operator *(Vector2 a, Vector2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        /// <summary>
        /// dot product of two vectors
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>dot product</returns>
        public static float Dot(Vector2 a, Vector2 b)
        {
            return a * b;
        }

        /// <summary>
        /// multiply a vector with a scalar
        /// </summary>
        /// <param name="vec">vector to multiply with scalar</param>
        /// <param name="scalar">to multiply vector with</param>
        /// <returns>result of vec*scalar</returns>
        public static Vector2 operator *(Vector2 vec, float scalar)
        {
            return new Vector2(vec.x * scalar, vec.y * scalar);
        }

        /// <summary>
        /// indexer for matrix
        /// </summary>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    default:
                        throw new IndexOutOfRangeException("Invalid vector index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid vector index!");
                }
            }
        }

        /// <summary>
        /// convert from Vector2 to Vector3
        /// </summary>
        /// <returns>returns vector3 where x,y = vec2.x,y and z = 0</returns>
        public Vector3 ToVector3()
        {
            return new Vector3(this);
        }


        /// <summary>
        /// rotates a given vector
        /// </summary>
        /// <param name="vec">vector to rotate</param>
        /// <param name="angle">angle to use for roation</param>
        /// <returns>the rotated vector</returns>
        public static Vector2 Rotate(Vector2 vec, float angle)
        {
            float cos = Math.Trigonometry.Cos(angle);
            float sin = Math.Trigonometry.Sin(angle);
            return new Vector2(vec.x * cos + vec.y * sin,
                               -vec.x * sin + vec.y * cos);
        }

        /// <summary>
        /// multiplies two vectors elementwise
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns></returns>
        public static Vector2 MultiplyElements(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        /// multiplies two vectors elementwise
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns></returns>
        public static Vector2 DivideElements(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x / b.x, a.y / b.y);
        }

        /// <summary>
        /// compares two vectors if they are equal
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>true if the two vectors are equal</returns>
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return (a.x == b.x && a.y == b.y);
        }


        /// <summary>
        /// compares two vectors if they are equal
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>true if the two vectors are equal</returns>
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return (a.x != b.x || a.y != b.y);
        }

        /// <summary>
        /// calculats the maximum of the two vectors
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns></returns>
        public static Vector2 Max(Vector2 a, Vector2 b)
        {
            return new Vector2(System.Math.Max(a.x, b.x), System.Math.Max(a.y, b.y));
        }

        /// <summary>
        /// Explicit conversion.
        /// </summary>
        /// <param name="size">The variable to convert.</param>
        /// <returns>The Vector2 object.</returns>
        public static explicit operator Vector2(System.Drawing.Size size)
        {
            return new Vector2(size.Width, size.Height);
        }
        //---------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------
    }
}
