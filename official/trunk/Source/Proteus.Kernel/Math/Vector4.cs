using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace Proteus.Kernel.Math
{
    public class Vector4Converter : ExpandableObjectConverter
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
              value is Vector4)
            {
                Vector4 pt = (Vector4)value;
                return pt.X.ToString(culture) + "; " + pt.Y.ToString(culture) + "; " +
                  pt.Z.ToString(culture) + "; " + pt.W.ToString(culture);
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
                    return new Vector4(float.Parse(elements[0], culture), float.Parse(elements[1], culture),
                      float.Parse(elements[2], culture), float.Parse(elements[3], culture));
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

    [System.ComponentModel.TypeConverter(typeof(Vector4Converter))]
    public struct Vector4
    {
        //---------------------------------------------------------------
        #region Variables
        //---------------------------------------------------------------
        private float x, y, z, w;
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

        /// <summary>Z element of the vector.</summary>
        public float Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }

        /// <summary>W element of the vector.</summary>
        public float W
        {
            get
            {
                return w;
            }
            set
            {
                w = value;
            }
        }

        /// <summary>
        /// returns the identity matrix
        /// </summary>
        [System.Diagnostics.DebuggerHidden]
        public static Vector4 Zero
        {
            get
            {
                return new Vector4();
            }
        }

        /// <summary>
        /// Returns a vector containing the first three components.
        /// </summary>
        public Vector3 Vector3
        {
            get
            {
                return new Vector3(x, y, z);
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
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// creates a new vector4 from a vector3
        /// </summary>
        /// <param name="vec">object to create vector4 from</param>
        public Vector4(Vector3 vec)
            : this(vec, 0.0f)
        {
        }

        /// <summary>
        /// creates a new vector4 from a vector3
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="w"></param>
        public Vector4(Vector3 vec, float w)
        {
            this.x = vec.X;
            this.y = vec.Y;
            this.z = vec.Z;
            this.w = w;
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
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();
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
            Vector4 vec = (Vector4)obj;
            return vec.x == x && vec.y == y && vec.z == z && vec.w == w;
        }

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <returns>The string representation of the object.</returns>
        public override string ToString()
        {
            return "X: " + x + " Y: " + y + " Z: " + z + " W: " + w;
        }

        /// <summary>
        /// calc unit vector from a certain Vector
        /// </summary>
        /// <param name="a">vector to calc unit vector from</param>
        /// <returns>normalized vector</returns>
        public static Vector4 Unit(Vector4 a)
        {
            return a / a.Length();
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
        /// linear interpolation between to vectors
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <param name="time">interpolation time [0..1]</param>
        /// <returns>interpolated vector</returns>
        public static Vector4 Lerp(Vector4 a, Vector4 b, float time)
        {
            return new Vector4(a.x + (b.x - a.x) * time,
              a.y + (b.y - a.y) * time,
              a.z + (b.z - a.z) * time,
              a.W + (b.W - a.W) * time);
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
        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        /// <summary>
        /// dot product of two vectors
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>dot product</returns>
        public static float operator *(Vector4 a, Vector4 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        /// <summary>
        /// multiply a vector with a scalar
        /// </summary>
        /// <param name="vec">vector to multiply with scalar</param>
        /// <param name="scalar">to multiply vector with</param>
        /// <returns>result of vec*scalar</returns>
        public static Vector4 operator *(Vector4 vec, float scalar)
        {
            return new Vector4(vec.x * scalar, vec.y * scalar, vec.z * scalar, vec.w * scalar);
        }

        /// <summary>
        /// dot product of two vectors
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>dot product</returns>
        public static float Dot(Vector4 a, Vector4 b)
        {
            return a * b;
        }

        /// <summary>
        /// transform a vector by a given Matrix
        /// </summary>
        /// <param name="v">vector to transform</param>
        /// <param name="m">matrix to use for Transformation</param>
        /// <returns>transformed vector</returns>
        public static Vector4 operator *(Vector4 v, Matrix4 m)
        {
            return new Vector4(v.x * m.A1 + v.y * m.B1 + v.z * m.C1 + v.w * m.D1,
              v.x * m.A2 + v.y * m.B2 + v.z * m.C2 + v.w * m.D2,
              v.x * m.A3 + v.y * m.B3 + v.z * m.C3 + v.w * m.D3,
              v.x * m.A4 + v.y * m.B4 + v.z * m.C4 + v.w * m.D4);
        }

        /// <summary>
        /// divide vector
        /// </summary>
        /// <param name="vec">vector to divide</param>
        /// <param name="divisor">divisor to divide vector with</param>
        /// <returns>vector/divisor</returns>
        public static Vector4 operator /(Vector4 vec, float divisor)
        {
            return new Vector4(vec.x / divisor, vec.y / divisor, vec.z / divisor, vec.w / divisor);
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
                    case 2:
                        return z;
                    case 3:
                        return w;
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
                    case 2:
                        z = value;
                        break;
                    case 3:
                        w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid vector index!");
                }
            }
        }
        //---------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------
    }
}
