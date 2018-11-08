using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public class Vector4
    {
        private float[] values;
        public Vector4(float x, float y, float z, float w = 0)
        {
            values = new float[4];
            values[0] = x; values[1] = y; values[2] = z; values[3] = w;
        }
        public Vector4(Vector3 v, float w = 0) : this(v.X, v.Y, v.Z, w)
        {
        }
        public Vector4(float[] v)
            : this(v[0], v[1], v[2], v[3])
        {
        }
        public float X { get { return values[0]; } set { values[0] = value; } }
        public float Y { get { return values[1]; } set { values[1] = value; } }
        public float Z { get { return values[2]; } set { values[2] = value; } }
        public float W { get { return values[3]; } set { values[3] = value; } }
        public float this[int idx]
        {
            get { return values[idx]; }
            set { values[idx] = value; }
        }

        public Vector4 Normalized
        {
            get { return W == 0 ? new Vector4(this) : new Vector4(X / W, Y / W, Z / W, W / W); }
        }

        public static Vector4 Zero()
        {
            return new Vector4(new float[4] { 0, 0, 0, 0 });
        }

        public static implicit operator Vector4(Vector3 v)
        {
            return new Vector4(v.X, v.Y, v.Z);
        }

        public static Vector4 operator *(Vector4 vector, float number)
        {
            var v = Zero();
            for (int i = 0; i < 4; i++)
                v[i] += vector[i] * number;
            return v;
        }

        public static Vector4 operator *(Matrix matrix, Vector4 vector)
        {
            var v = Zero();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    v[i] += matrix[i, j] * vector[j];
            return v;
        }
    }
}

