using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Proteus.Kernel.Math
{
    [System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public struct Matrix3
    {
        //---------------------------------------------------------------
        #region Variables
        //---------------------------------------------------------------
        /// <summary>first row</summary>
        public float A1, A2, A3;
        /// <summary>second row</summary>
        public float B1, B2, B3;
        /// <summary>third row</summary>
        public float C1, C2, C3;
        //---------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------

        //---------------------------------------------------------------
        #region Properties
        //---------------------------------------------------------------
        /// <summary>
        /// returns the identity matrix
        /// </summary>
        public static Matrix3 Identity
        {
            get
            {
                Matrix3 m = new Matrix3();
                m.A1 = m.B2 = m.C3 = 1.0f;
                return m;
            }
        }

        /// <summary>
        /// returns a matrix filled with 0.0f
        /// </summary>
        public static Matrix3 Zero
        {
            get
            {
                return new Matrix3();
            }
        }

        /// <summary>
        /// returns/sets the first column of the matrix
        /// </summary>
        public Vector3 Column1
        {
            get
            {
                return new Vector3(A1, B1, C1);
            }
            set
            {
                A1 = value.X;
                B1 = value.Y;
                C1 = value.Z;
            }
        }

        /// <summary>
        /// returns/sets the second column of the matrix
        /// </summary>
        public Vector3 Column2
        {
            get
            {
                return new Vector3(A2, B2, C2);
            }
            set
            {
                A2 = value.X;
                B2 = value.Y;
                C2 = value.Z;
            }
        }

        /// <summary>
        /// returns/sets the third column of the matrix
        /// </summary>
        public Vector3 Column3
        {
            get
            {
                return new Vector3(A3, B3, C3);
            }
            set
            {
                A3 = value.X;
                B3 = value.Y;
                C3 = value.Z;
            }
        }

        /// <summary>
        /// returns the lookAt vector of the matrix
        /// </summary>
        public Vector3 LookAtVector
        {
            get
            {
                return new Vector3(A3, B3, C3);
            }
            set
            {
                A3 = value.X;
                B3 = value.Y;
                C3 = value.Z;
            }
        }

        /// <summary>
        /// returns the up vector of the matrix
        /// </summary>
        public Vector3 UpVector
        {
            get
            {
                return new Vector3(A2, B2, C2);
            }
            set
            {
                A2 = value.X;
                B2 = value.Y;
                C2 = value.Z;
            }
        }

        /// <summary>
        /// returns the orthogonal-vector of up and look
        /// </summary>
        public Vector3 RightVector
        {
            get
            {
                return new Vector3(A1, B1, C1);
            }
            set
            {
                A1 = value.X;
                B1 = value.Y;
                C1 = value.Z;
            }
        }

        /// <summary>
        /// converts matrix to quaternion
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public Quaternion Quaternion
        {
            get
            {
                float diagonal = A1 + B2 + C3;
                float scale = 0.0f;
                float x, y, z, w;

                // If the diagonal is greater than zero
                if (diagonal > 0.00000001)
                {
                    // Calculate the scale of the diagonal
                    scale = Basic.Sqrt(diagonal) * 2;

                    // Calculate the x, y, x and w of the quaternion through the respective equation
                    x = (B3 - C2) / scale;
                    y = (C1 - A3) / scale;
                    z = (A2 - B1) / scale;
                    w = 0.25f * scale;
                }
                else
                {
                    // If the first element of the diagonal is the greatest value
                    if (A1 > B2 && A1 > C3)
                    {
                        // Find the scale according to the first element, and double that value
                        scale = Basic.Sqrt(1.0f + A1 - B2 - C3) * 2.0f;

                        // Calculate the x, y, x and w of the quaternion through the respective equation
                        x = 0.25f * scale;
                        y = (A2 + B1) / scale;
                        z = (C1 + A3) / scale;
                        w = (B3 - C2) / scale;
                    }
                    // Else if the second element of the diagonal is the greatest value
                    else if (B2 > C3)
                    {
                        // Find the scale according to the second element, and double that value
                        scale = Basic.Sqrt(1.0f + B2 - A1 - C3) * 2.0f;

                        // Calculate the x, y, x and w of the quaternion through the respective equation
                        x = (A2 + B1) / scale;
                        y = 0.25f * scale;
                        z = (B3 + C2) / scale;
                        w = (C1 - A3) / scale;
                    }
                    // Else the third element of the diagonal is the greatest value
                    else
                    {
                        // Find the scale according to the third element, and double that value
                        scale = Basic.Sqrt(1.0f + C3 - A1 - B2) * 2.0f;

                        // Calculate the x, y, x and w of the quaternion through the respective equation
                        x = (C1 + A3) / scale;
                        y = (B3 + C2) / scale;
                        z = 0.25f * scale;
                        w = (A2 - B1) / scale;
                    }
                }
                return new Quaternion(x, y, z, w);
            }
        }
        //---------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------

        //---------------------------------------------------------------
        #region Initialisation
        //---------------------------------------------------------------
        /// <summary>
        /// constructor for fillin all elements
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="a3"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="b3"></param>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="c3"></param>
        public Matrix3(float a1, float a2, float a3,
          float b1, float b2, float b3,
          float c1, float c2, float c3)
        {
            A1 = a1; B1 = b1; C1 = c1;
            A2 = a2; B2 = b2; C2 = c2;
            A3 = a3; B3 = b3; C3 = c3;
        }

        /// <summary>
        /// build matrix from vectors
        /// </summary>
        /// <param name="A">first row</param>
        /// <param name="B">second row</param>
        /// <param name="C">third row</param>
        public Matrix3(Vector3 A, Vector3 B, Vector3 C)
        {
            A1 = A.X; B1 = B.X; C1 = C.X;
            A2 = A.Y; B2 = B.Y; C2 = C.Y;
            A3 = A.Z; B3 = B.Z; C3 = C.Z;
        }

        //---------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------

        //---------------------------------------------------------------
        #region Methods
        //---------------------------------------------------------------
        /// <summary>
        /// retuns the rotation matrix around the x-axis
        /// </summary>
        /// <param name="alpha">angle in rad</param>
        /// <returns>rotation matrix around x-axis</returns>
        public static Matrix3 RotationX(float alpha)
        {
            float cos = Math.Trigonometry.Cos(alpha);
            float sin = Math.Trigonometry.Sin(alpha);
            return new Matrix3(1, 0, 0,
              0, cos, sin,
              0, -sin, cos);
        }

        /// <summary>
        /// retuns the rotation matrix around the y-axis
        /// </summary>
        /// <param name="alpha">angle in rad</param>
        /// <returns>rotation matrix around y-axis</returns>
        public static Matrix3 RotationY(float alpha)
        {
            float cos = Math.Trigonometry.Cos(alpha);
            float sin = Math.Trigonometry.Sin(alpha);
            return new Matrix3(cos, 0, -sin,
              0, 1, 0,
              sin, 0, cos);
        }

        /// <summary>
        /// retuns the rotation matrix around the z-axis
        /// </summary>
        /// <param name="alpha">angle in rad</param>
        /// <returns>rotation matrix around z-axis</returns>
        public static Matrix3 RotationZ(float alpha)
        {
            float cos = Math.Trigonometry.Cos(alpha);
            float sin = Math.Trigonometry.Sin(alpha);
            return new Matrix3(cos, sin, 0,
              -sin, cos, 0,
              0, 0, 1);
        }

        /// <summary>
        /// returns the rotation matrix for alpha, beta and gamma
        /// </summary>
        /// <param name="alpha">around x-Axis: yaw</param>
        /// <param name="beta">around y-Axis: pitch</param>
        /// <param name="gamma">around z-Axis: role</param>
        public static Matrix3 Rotation(float alpha, float beta, float gamma)
        {
            // some constants	
            float sa = Math.Trigonometry.Sin(alpha);
            float ca = Math.Trigonometry.Cos(alpha);
            float sb = Math.Trigonometry.Sin(beta);
            float cb = Math.Trigonometry.Cos(beta);
            float sg = Math.Trigonometry.Sin(gamma);
            float cg = Math.Trigonometry.Cos(gamma);
            float sbsa = sb * sa;
            float sbca = sb * ca;

            Matrix3 m = new Matrix3();

            m.A1 = cg * cb;
            m.A2 = sg * cb;
            m.A3 = -sb;
            m.B1 = cg * sbsa - sg * ca;
            m.B2 = sg * sbsa + cg * ca;
            m.B3 = cb * sa;
            m.C1 = cg * sbca + sg * sa;
            m.C2 = sg * sbca - cg * sa;
            m.C3 = cb * ca;
            return m;
        }

        /// <summary>
        /// builds scale matrix
        /// </summary>
        /// <param name="value">value to scale</param>
        /// <returns>scale matrix</returns>
        public static Matrix3 Scaling(float value)
        {
            return Scaling(new Vector3(value, value, value));
        }

        /// <summary>
        /// builds scale matrix
        /// </summary>
        /// <param name="vec">value to scale</param>
        /// <returns>scale matrix</returns>
        public static Matrix3 Scaling(Vector3 vec)
        {
            Matrix3 m = new Matrix3();
            m.A1 = vec.X;
            m.B2 = vec.Y;
            m.C3 = vec.Z;
            return m;
        }

        /// <summary>
        /// Builds a left-handed, look-at matrix.
        /// </summary>
        /// <param name="eye">vector that defines the eye point. This value is used in translation.</param>
        /// <param name="at">vector that defines the camera look-at target</param>
        /// <param name="up">vector that defines the current world's up, usually [0, 1, 0]. </param>
        /// <returns>left-handed, look-at matrix</returns>
        public static Matrix3 LookAt(Vector3 eye, Vector3 at, Vector3 up)
        {
            Vector3 zAxis = Vector3.Unit(at - eye);
            Vector3 xAxis = Vector3.Unit(Vector3.Cross(up, zAxis));
            Vector3 yAxis = Vector3.Cross(zAxis, xAxis);

            return new Matrix3(xAxis.X, xAxis.Y, xAxis.Z,
              yAxis.X, yAxis.Y, yAxis.Z,
              zAxis.X, zAxis.Y, zAxis.Z);

        }

        /// <summary>
        /// builds a rotation matrix for a given vector and an angle
        /// </summary>
        /// <param name="vec">to rotate around</param>
        /// <param name="angle">rotation angle</param>
        /// <returns>rotation matrix</returns>
        public static Matrix3 Rotation(Vector3 vec, float angle)
        {
            float c = Math.Trigonometry.Cos(angle);
            float s = Math.Trigonometry.Sin(angle);
            float t = 1 - c;
            Matrix3 m =
              new Matrix3(t * vec.X * vec.X + c, t * vec.X * vec.Y - s * vec.Z, t * vec.X * vec.Z + s * vec.Y,
              t * vec.X * vec.Y + s * vec.Z, t * vec.Y * vec.Y + c, t * vec.Y * vec.Z - s * vec.X,
              t * vec.X * vec.Z - s * vec.Y, t * vec.Y * vec.Z + s * vec.X, t * vec.Z * vec.Z + c);
            return Matrix3.Transpose(m);
        }


        /// <summary>
        /// returns the transposed matrix
        /// </summary>
        /// <param name="matrix">matrix to transpose</param>
        /// <returns>the transposed matrix</returns>
        public static Matrix3 Transpose(Matrix3 matrix)
        {
            Matrix3 m = new Matrix3();
            m.A1 = matrix.A1; m.A2 = matrix.B1; m.A3 = matrix.C1;
            m.B1 = matrix.A2; m.B2 = matrix.B2; m.B3 = matrix.C2;
            m.C1 = matrix.A3; m.C2 = matrix.B3; m.C3 = matrix.C3;
            return m;
        }

        /// <summary>
        /// calcluates the deterimant of the matrix
        /// </summary>
        /// <returns></returns>
        public float Det()
        {
            // rule of Sarrus
            return A1 * B2 * C3 + A2 * B3 * C1 + A3 * B1 * C2 -
              A3 * B2 * C1 - A1 * B3 * C2 - A2 * B1 * C3;
        }

        /// <summary>
        /// spherical interpolation between to matrices
        /// </summary>
        /// <param name="a">first matrix</param>
        /// <param name="b">second matrix</param>
        /// <param name="time">interpolation time [0..1]</param>
        /// <returns>interpolated matrix</returns>
        public static Matrix3 Slerp(Matrix3 a, Matrix3 b, float time)
        {

            Quaternion qa = a.Quaternion;
            Quaternion qb = b.Quaternion;

            Quaternion interpolated = Quaternion.Slerp(qa, qb, time);

            Matrix3 result = interpolated.Matrix;
            return result;
        }
        //---------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------

        //---------------------------------------------------------------
        #region Operations
        //---------------------------------------------------------------
        /// <summary>
        /// add two matrices
        /// </summary>
        /// <param name="a">matrix a</param>
        /// <param name="b">matrix b</param>
        /// <returns>sum of the two matrices</returns>
        public static Matrix3 operator +(Matrix3 a, Matrix3 b)
        {
            return new Matrix3(a.A1 + b.A1, a.A2 + b.A2, a.A3 + b.A3,
              a.B1 + b.B1, a.B2 + b.B2, a.B3 + b.B3,
              a.C1 + b.C1, a.C2 + b.C2, a.C3 + b.C3);
        }

        /// <summary>
        /// indexer for matrix
        /// </summary>
        public float this[int column, int row]
        {
            get
            {
                return this[column + row * 3];
            }
            set
            {
                this[column + row * 3] = value;
            }
        }

        /// <summary>
        /// indexer for matrix
        /// </summary>
        public float this[int index]
        {
            get
            {
                /*fixed( float *f = &this.A1 ) {
          return *(f+index);
        }*/

                switch (index)
                {
                    case 0:
                        return A1;
                    case 1:
                        return A2;
                    case 2:
                        return A3;
                    case 3:
                        return B1;
                    case 4:
                        return B2;
                    case 5:
                        return B3;
                    case 6:
                        return C1;
                    case 7:
                        return C2;
                    case 8:
                        return C3;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
            set
            {
                /*fixed( float *f = &this.A1 ) {
          *(f+index) = value;
        }*/
                switch (index)
                {
                    case 0:
                        A1 = value;
                        break;
                    case 1:
                        A2 = value;
                        break;
                    case 2:
                        A3 = value;
                        break;
                    case 3:
                        B1 = value;
                        break;
                    case 4:
                        B2 = value;
                        break;
                    case 5:
                        B3 = value;
                        break;
                    case 6:
                        C1 = value;
                        break;
                    case 7:
                        C2 = value;
                        break;
                    case 8:
                        C3 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
        }

        /// <summary>
        /// multiplies two matrices
        /// </summary>
        /// <param name="a">first matrix</param>
        /// <param name="b">second matrix</param>
        /// <returns>result matrix</returns>
        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
        {
            Matrix3 result = Matrix3.Zero;

            // TODO: optimize!!! ;)
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    for (int i = 0; i < 3; i++)
                        result[x, y] += a[i, y] * b[x, i];

                          return result;
                    }

        //---------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------
    }
}
