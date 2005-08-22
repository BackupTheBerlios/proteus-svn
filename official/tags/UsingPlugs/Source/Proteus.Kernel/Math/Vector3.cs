using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace Proteus.Kernel.Math
{
    public class Vector3Converter : ExpandableObjectConverter
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
              value is Vector3)
            {
                Vector3 pt = (Vector3)value;
                return pt.X.ToString(culture) + "; " + pt.Y.ToString(culture) + "; " + pt.Z.ToString(culture);
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
                    return new Vector3(float.Parse(elements[0], culture), float.Parse(elements[1], culture),
                      float.Parse(elements[2], culture));
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

    [System.ComponentModel.TypeConverter(typeof(Vector3Converter))]
    public struct Vector3
    {
        //---------------------------------------------------------------
        #region Variables
        //---------------------------------------------------------------
        private float x, y, z;
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

        /// <summary>
        /// returns the identity matrix
        /// </summary>
        public static Vector3 Zero
        {
            get
            {
                return new Vector3();
            }
        }

        /// <summary>
        /// a column of matrix
        /// </summary>
        public static Vector3 LookAt
        {
            get
            {
                return new Vector3(0, 0, 1);
            }
        }

        /// <summary>
        /// b column of matrix
        /// </summary>
        public static Vector3 Up
        {
            get
            {
                return new Vector3(0, 1.0f, 0);
            }
        }

        /// <summary>
        /// c column of matrix
        /// </summary>
        public static Vector3 Right
        {
            get
            {
                return new Vector3(1, 0, 0);
            }
        }

        /// <summary>
        /// Returns a vector containing the first three components.
        /// </summary>
        public Vector2 Vector2
        {
            get
            {
                return new Vector2(x, y);
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
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// construct vector3 from vector2
        /// z component is filled with 0
        /// </summary>
        /// <param name="vec2"></param>
        public Vector3(Vector2 vec2)
        {
            this.x = vec2.X;
            this.y = vec2.Y;
            this.z = 0;
        }

        /// <summary>
        /// construct vector3 from vector2
        /// z component is filled with 0
        /// </summary>
        /// <param name="vec2"></param>
        /// <param name="z"></param>
        public Vector3(Vector2 vec2, float z)
        {
            this.x = vec2.X;
            this.y = vec2.Y;
            this.z = z;
        }
        /// <summary>
        /// Creates a vector from a byte array.
        /// </summary>
        /// <param name="bytes">Array of at least 12 byte elements.</param>
        /// <returns>Created vector.</returns>
        public static Vector3 From(byte[] bytes)
        {
            return From(bytes, 0);
        }


        /// <summary>
        /// Creates a vector from a byte array.
        /// </summary>
        /// <param name="bytes">Array of at least 12 byte elements.</param>
        /// <param name="offset">Start offset in buffer.</param>
        /// <returns>Created vector.</returns>
        public unsafe static Vector3 From(byte[] bytes, int offset)
        {
            //System.Diagnostics.Debug.Assert(bytes.Length >= (12 + offset));
            fixed (byte* p = &bytes[offset])
            {
                float* floatPtr = (float*)(p);
                return new Vector3(floatPtr[0], floatPtr[1], floatPtr[2]);
            }
        }
        //---------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------

        //---------------------------------------------------------------
        #region Methods
        //---------------------------------------------------------------
        /// <summary>
        /// Adds the weighted skinned source vector.
        /// </summary>
        public void AddSkinned(Vector3 sourceVec, ref Matrix4 joint, float weight)
        {
            x += (sourceVec.x * joint.A1 + sourceVec.y * joint.B1 + sourceVec.z * joint.C1 + joint.D1) * weight;
            y += (sourceVec.x * joint.A2 + sourceVec.y * joint.B2 + sourceVec.z * joint.C2 + joint.D2) * weight;
            z += (sourceVec.x * joint.A3 + sourceVec.y * joint.B3 + sourceVec.z * joint.C3 + joint.D3) * weight;
        }

        /// <summary>
        /// new hashcode function
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
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
            Vector3 vec = (Vector3)obj;
            return vec.x == x && vec.y == y && vec.z == z;
        }

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <returns>The string representation of the object.</returns>
        public override string ToString()
        {
            return "X: " + x + " Y: " + y + " Z: " + z;
        }

        /// <summary>
        /// calc unit vector from a certain Vector
        /// </summary>
        /// <param name="a">vector to calc unit vector from</param>
        /// <returns>unit vector</returns>
        public static Vector3 Unit(Vector3 a)
        {
            float length = 1 / a.Length();
            return a * length;
        }

        /// <summary>
        /// normalizes the current instance
        /// </summary>
        public void Normalize()
        {
            float length = 1 / Length();
            this.x = x * length;
            this.y = y * length;
            this.z = z * length;
        }

        /// <summary>
        /// Returns the length of the vector.
        /// </summary>
        /// <returns>Length of the vector.</returns>
        public float Length()
        {
            return Basic.Sqrt(this * this);
        }

        /// <summary>
        /// Returns the squared length of the vector.
        /// </summary>
        /// <returns>Squared length of the vector.</returns>
        public float LengthSquared()
        {
            return this * this;
        }

        /// <summary>
        /// calcs the cross product of two vectors
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>normal vector of a and b</returns>
        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3(
              a.y * b.z - a.z * b.y,
              a.z * b.x - a.x * b.z,
              a.x * b.y - a.y * b.x);
        }

        /// <summary>
        /// calcs the cross product of two vectors
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>normal vector of a and b</returns>
        public static Vector3 CrossUnit(Vector3 a, Vector3 b)
        {
            Vector3 vec = new Vector3(
              a.y * b.z - a.z * b.y,
              a.z * b.x - a.x * b.z,
              a.x * b.y - a.y * b.x);
            float length = Math.Basic.InvSqrt(vec.x * vec.x + vec.y * vec.y * vec.z * vec.z);
            vec.x = vec.x * length;
            vec.y = vec.y * length;
            vec.z = vec.z * length;
            return vec;
        }

        /// <summary>
        /// creates a vector from an array
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vector3 FromArray(float[] vec)
        {
            //System.Diagnostics.Debug.Assert(vec.Length == 3, "Array.Length != 3");
            return new Vector3(vec[0], vec[1], vec[2]);
        }

        /// <summary>
        /// linear interpolation between to vectors
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <param name="time">interpolation time [0..1]</param>
        /// <returns>interpolated vector</returns>
        public static Vector3 Lerp(Vector3 a, Vector3 b, float time)
        {
            return new Vector3(a.x + (b.x - a.x) * time,
              a.y + (b.y - a.y) * time,
              a.z + (b.z - a.z) * time);
        }

        /// <summary>
        /// Sets the vector to zero.
        /// </summary>
        public void SetZero()
        {
            x = 0;
            y = 0;
            z = 0;
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
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        /// <summary>
        /// Adds a certain vector to the current vector.
        /// </summary>
        /// <param name="a">The vector to add.</param>
        public void Add(Vector3 a)
        {
            x += a.x;
            y += a.y;
            z += a.z;
        }

        /// <summary>
        /// Adds a weighted vector.
        /// </summary>
        /// <param name="a">The vector to add.</param>
        /// <param name="scalar">The scalar to multiply vector beforea adding.</param>
        public void AddWeighted(Vector3 a, float scalar)
        {
            x += a.x * scalar;
            y += a.y * scalar;
            z += a.z * scalar;
        }

        /// <summary>
        /// subtracts Vector be from Vector a
        /// </summary>
        /// <param name="a">vector to subtract from</param>
        /// <param name="b">vector to subtract</param>
        /// <returns>result of subtraction</returns>
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        /// <summary>
        /// Negate vector
        /// </summary>
        /// <param name="a">vector to negate</param>
        /// <returns>negative vector</returns>
        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.x, -a.y, -a.z);
        }

        /// <summary>
        /// divide vector
        /// </summary>
        /// <param name="vec">vector to divide</param>
        /// <param name="divisor">divisor to divide vector with</param>
        /// <returns>vector/divisor</returns>
        public static Vector3 operator /(Vector3 vec, float divisor)
        {
            return new Vector3(vec.x / divisor, vec.y / divisor, vec.z / divisor);
        }

        /// <summary>
        /// scalar product
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>scalar product</returns>
        public static float operator *(Vector3 a, Vector3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        /// <summary>
        /// dot product of two vectors
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns>dot product</returns>
        public static float Dot(Vector3 a, Vector3 b)
        {
            return a * b;
        }

        /// <summary>
        /// transform a vector by a given Matrix
        /// </summary>
        /// <param name="m">matrix to use for Transformation</param>
        /// <param name="v">vector to transform</param>		
        /// <returns>transformed vector</returns>
        public static Vector3 operator *(Vector3 v, Matrix4 m)
        {
            Vector3 vec = new Vector3(v.x * m.A1 + v.y * m.B1 + v.z * m.C1 + m.D1,
              v.x * m.A2 + v.y * m.B2 + v.z * m.C2 + m.D2,
              v.x * m.A3 + v.y * m.B3 + v.z * m.C3 + m.D3);
            return vec;
        }

        /// <summary>
        /// Transforms the current vector by a matrix.
        /// </summary>
        /// <param name="m">Matrix to transform current vector with.</param>
        public void Mul(ref Matrix4 m)
        {
            float ox = x;
            float oy = y;
            float oz = z;

            x = ox * m.A1 + oy * m.B1 + oz * m.C1 + m.D1;
            y = ox * m.A2 + oy * m.B2 + oz * m.C2 + m.D2;
            z = ox * m.A3 + oy * m.B3 + oz * m.C3 + m.D3;
        }

        /// <summary>
        /// transform a vector by a given Matrix
        /// </summary>
        /// <param name="m">matrix to use for Transformation</param>
        /// <param name="v">vector to transform</param>		
        /// <returns>transformed vector</returns>
        public static Vector3 operator *(Vector3 v, Matrix3 m)
        {
            return new Vector3(v.x * m.A1 + v.y * m.B1 + v.z * m.C1,
              v.x * m.A2 + v.y * m.B2 + v.z * m.C2,
              v.x * m.A3 + v.y * m.B3 + v.z * m.C3);
        }

        /// <summary>
        /// scales the vector by a scale vector
        /// </summary>
        /// <param name="a">the vector to scale</param>
        /// <param name="b">the scale vector</param>
        /// <returns>the element wise multiplication of the two vectors</returns>
        public static Vector3 Scale(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        /// tests if all elements of one vector are smaller than all of another vector
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns></returns>
        public static bool AllLess(Vector3 a, Vector3 b)
        {
            return a.x < b.x && a.y < b.y && a.z < b.z;
        }

        /// <summary>
        /// tests if all elements of one vector are smaller than all of another vector
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns></returns>
        public static bool AllLessOrEqual(Vector3 a, Vector3 b)
        {
            return a.x <= b.x && a.y <= b.y && a.z <= b.z;
        }

        /// <summary>
        /// test if at least one element of a is smaller than in b
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns></returns>
        public static bool OneLess(Vector3 a, Vector3 b)
        {
            return a.x < b.x || a.y < b.y || a.z < b.z;
        }

        /// <summary>
        /// test if at least one element of a is smaller than in b
        /// </summary>
        /// <param name="a">first vector</param>
        /// <param name="b">second vector</param>
        /// <returns></returns>
        public static bool OneLessOrEqual(Vector3 a, Vector3 b)
        {
            return a.x <= b.x || a.y <= b.y || a.z <= b.z;
        }

        /// <summary>
        /// multiply a vector with a scalar
        /// </summary>
        /// <param name="vec">vector to multiply with scalar</param>
        /// <param name="scalar">to multiply vector with</param>
        /// <returns>result of vec*scalar</returns>
        public static Vector3 operator *(Vector3 vec, float scalar)
        {
            return new Vector3(vec.x * scalar, vec.y * scalar, vec.z * scalar);
        }

        /// <summary>
        /// Multiplies the current vector with a scalar.
        /// </summary>
        /// <param name="scalar"></param>
        public void Mul(float scalar)
        {
            x = x * scalar;
            y = y * scalar;
            z = z * scalar;
        }

        /// <summary>
        /// multiply a vector with a scalar
        /// </summary>
        /// <param name="scalar">to multiply vector with</param>
        /// <param name="vec">vector to multiply with scalar</param>
        /// <returns>result of scalar*vec</returns>
        public static Vector3 operator *(float scalar, Vector3 vec)
        {
            return new Vector3(vec.x * scalar, vec.y * scalar, vec.z * scalar);
        }

        /// <summary>
        /// tests if two vectors are the same
        /// </summary>
        /// <param name="vec">first vector to compare</param>
        /// <param name="vec2">second vector ti compare</param>
        /// <returns>true if the elements of the vectors are the same</returns>
        public static bool operator ==(Vector3 vec, Vector3 vec2)
        {
            return (vec.x == vec2.x && vec.y == vec2.y && vec.z == vec2.z);
        }

        /// <summary>
        /// tests if two vectors are different
        /// </summary>
        /// <param name="vec">first vector to compare</param>
        /// <param name="vec2">second vector ti compare</param>
        /// <returns>true if at least one elements of vec is different from vec2</returns>
        public static bool operator !=(Vector3 vec, Vector3 vec2)
        {
            return (vec.x != vec2.x || vec.y != vec2.y || vec.z != vec2.z);
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
