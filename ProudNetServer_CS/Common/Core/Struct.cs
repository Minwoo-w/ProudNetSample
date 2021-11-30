using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public struct Transform
    {
        public Vector3 position;
        // ToDo - Rotation

        public override string ToString()
        {
            return $"{position}";
        }
    }

    // Vector3
    public struct Vector3
    {
        public static Vector3 zero = new Vector3(0.0f, 0.0f, 0.0f);
        public static Vector3 one = new Vector3(0.0f, 0.0f, 0.0f);

        public float x;
        public float y;
        public float z;

        public Vector3(float x = 0.0f, float y = 0.0f, float z = 0.0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(int x, int y, int z)
        {
            this.x = (float)x * 0.01f;
            this.y = (float)y * 0.01f;
            this.z = (float)z * 0.01f;
        }

        public override string ToString()
        {
            return $"{{ {x}, {y}, {z} }}";
        }

        // Math

        public float Magnitude() // 벡터 크기
        {
            double magnitude = Math.Sqrt(x * x + y * y + z * z); 
            return (float)magnitude;
        }

        public Vector3 Normalrize() // 정규화(단위벡터로 만들기)
        { 
            Vector3 normal = new Vector3(); 
            float magnitude = Magnitude(); 
            if (magnitude != 0.0f)
            {
                normal.x = x / magnitude;
                normal.y = y / magnitude;
                normal.z = z / magnitude;
            }
            return normal; 
        }

        public string ToSerializable()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((int)(x * 100.0f));
            sb.Append((int)(x * 100.0f));
            sb.Append((int)(x * 100.0f));
            return sb.ToString();
        }

        // Operator
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            Vector3 result = v1;
            result.x += v2.x;
            result.y += v2.y;
            result.z += v2.z;
            return result;
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            Vector3 result = v1;
            result.x -= v2.x;
            result.y -= v2.y;
            result.z -= v2.z;
            return result;
        }

        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            if (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z)
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            if (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z)
            {
                return false;
            }

            return true;
        }

        public static Vector3 operator *(Vector3 v1, float value)
        {
            Vector3 result = v1;
            result.x *= value;
            result.y *= value;
            result.z *= value;
            return result;
        } 
    }
}
