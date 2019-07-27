using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace SheepReaper.GameSaves
{
    public struct Vector2I : IEquatable<Vector2I>, IFormattable
    {
        public int X;
        public int Y;

        [JitIntrinsic]
        public Vector2I(int value) : this(value, value) { }

        [JitIntrinsic]
        public Vector2I(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2I One => new Vector2I(1, 1);
        public static Vector2I UnitX => new Vector2I(1, 0);
        public static Vector2I UnitY => new Vector2I(0, 1);
        public static Vector2I Zero => new Vector2I();

        [JitIntrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Abs(Vector2I value)
        {
            return new Vector2I(Math.Abs(value.X), Math.Abs(value.Y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Add(Vector2I left, Vector2I right)
        {
            return left + right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Clamp(Vector2I value1, Vector2I min, Vector2I max)
        {
            // This compare order is very important!!!
            // We must follow HLSL behavior in the case user specified min value is bigger than max value.
            var x = value1.X;
            x = x > max.X ? max.X : x;
            x = x < min.X ? min.X : x;

            var y = value1.Y;
            y = y > max.Y ? max.Y : y;
            y = y < min.Y ? min.Y : y;

            return new Vector2I(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(Vector2I value1, Vector2I value2)
        {
            if (Vector.IsHardwareAccelerated)
            {
                var difference = value1 - value2;
                var ls = Dot(difference, difference);
                return Math.Sqrt(ls);
            }
            else
            {
                var dx = value1.X - value2.X;
                var dy = value1.Y - value2.Y;

                var ls = dx * dx + dy * dy;

                return Math.Sqrt(ls);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int DistanceSquared(Vector2I value1, Vector2I value2)
        {
            if (Vector.IsHardwareAccelerated)
            {
                var difference = value1 - value2;
                return Dot(difference, difference);
            }
            else
            {
                var dx = value1.X - value2.X;
                var dy = value1.Y - value2.Y;

                return dx * dx + dy * dy;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Divide(Vector2I left, Vector2I right)
        {
            return left / right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Divide(Vector2I left, int divisor)
        {
            return left / divisor;
        }

        [JitIntrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Dot(Vector2I value1, Vector2I value2)
        {
            return value1.X * value2.X + value1.Y * value2.Y;
        }

        public static implicit operator Vector2(Vector2I vector)
        {
            return new Vector2
            {
                X = vector.X,
                Y = vector.Y
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Lerp(Vector2I value1, Vector2I value2, int amount)
        {
            return new Vector2I(
                value1.X + (value2.X - value1.X) * amount,
                value1.Y + (value2.Y - value1.Y) * amount);
        }

        [JitIntrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Max(Vector2I value1, Vector2I value2)
        {
            return new Vector2I(
                value1.X > value2.X ? value1.X : value2.X,
                value1.Y > value2.Y ? value1.Y : value2.Y);
        }

        [JitIntrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Min(Vector2I value1, Vector2I value2)
        {
            return new Vector2I(
                value1.X < value2.X ? value1.X : value2.X,
                value1.Y < value2.Y ? value1.Y : value2.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Multiply(Vector2I left, Vector2I right)
        {
            return left * right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Multiply(Vector2I left, int right)
        {
            return left * right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Multiply(int left, Vector2I right)
        {
            return left * right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Negate(Vector2I value)
        {
            return -value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Normalize(Vector2I value)
        {
            if (Vector.IsHardwareAccelerated)
            {
                var length = value.Length();
                return value / length;
            }
            else
            {
                var ls = value.X * value.X + value.Y * value.Y;
                var invNorm = 1.0 / Math.Sqrt(ls);

                return new Vector2I
                {
                    X = Convert.ToInt32(value.X * invNorm),
                    Y = Convert.ToInt32(value.Y * invNorm)
                };
            }
        }

        [JitIntrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I operator -(Vector2I left, Vector2I right)
        {
            return new Vector2I(left.X - right.X, left.Y - right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I operator -(Vector2I value)
        {
            return Zero - value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector2I left, Vector2I right)
        {
            return !(left == right);
        }

        [JitIntrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I operator *(int left, Vector2I right)
        {
            return new Vector2I(left, left) * right;
        }

        [JitIntrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I operator *(Vector2I left, Vector2I right)
        {
            return new Vector2I(left.X * right.X, left.Y * right.Y);
        }

        [JitIntrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I operator *(Vector2I left, int right)
        {
            return left * new Vector2I(right, right);
        }

        [JitIntrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I operator /(Vector2I value1, int value2)
        {
            var invDiv = 1.0 / value2;
            return new Vector2I
            {
                X = Convert.ToInt32(value1.X * invDiv),
                Y = Convert.ToInt32(value1.Y * invDiv)
            };
        }

        [JitIntrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I operator /(Vector2I left, Vector2I right)
        {
            return new Vector2I(Convert.ToInt32((double)left.X / right.X), Convert.ToInt32((double)left.Y / right.Y));
        }

        [JitIntrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I operator +(Vector2I left, Vector2I right)
        {
            return new Vector2I(left.X + right.X, left.Y + right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector2I left, Vector2I right)
        {
            return left.Equals(right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Reflect(Vector2I vector, Vector2I normal)
        {
            if (Vector.IsHardwareAccelerated)
            {
                var dot = Dot(vector, normal);
                return vector - 2 * dot * normal;
            }
            else
            {
                var dot = vector.X * normal.X + vector.Y * normal.Y;

                return new Vector2I
                {
                    X = vector.X - 2 * dot * normal.X,
                    Y = vector.Y - 2 * dot * normal.Y
                };
            }
        }

        [JitIntrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I SquareRoot(Vector2I value)
        {
            return new Vector2I(Convert.ToInt32(Math.Sqrt(value.X)), Convert.ToInt32(Math.Sqrt(value.Y)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Subtract(Vector2I left, Vector2I right)
        {
            return left - right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Transform(Vector2I position, Matrix3x2 matrix)
        {
            return new Vector2I(
                Convert.ToInt32(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M31),
                Convert.ToInt32(position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M32));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Transform(Vector2I position, Matrix4x4 matrix)
        {
            return new Vector2I(
                Convert.ToInt32(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41),
                Convert.ToInt32(position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I Transform(Vector2I value, Quaternion rotation)
        {
            var x2 = rotation.X + rotation.X;
            var y2 = rotation.Y + rotation.Y;
            var z2 = rotation.Z + rotation.Z;

            var wz2 = rotation.W * z2;
            var xx2 = rotation.X * x2;
            var xy2 = rotation.X * y2;
            var yy2 = rotation.Y * y2;
            var zz2 = rotation.Z * z2;

            return new Vector2I(
                Convert.ToInt32(value.X * (1 - yy2 - zz2) + value.Y * (xy2 - wz2)),
                Convert.ToInt32(value.X * (xy2 + wz2) + value.Y * (1 - xx2 - zz2)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I TransformNormal(Vector2I normal, Matrix3x2 matrix)
        {
            return new Vector2I(
                Convert.ToInt32(normal.X * matrix.M11 + normal.Y * matrix.M21),
                Convert.ToInt32(normal.X * matrix.M12 + normal.Y * matrix.M22));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2I TransformNormal(Vector2I normal, Matrix4x4 matrix)
        {
            return new Vector2I(
                Convert.ToInt32(normal.X * matrix.M11 + normal.Y * matrix.M21),
                Convert.ToInt32(normal.X * matrix.M12 + normal.Y * matrix.M22));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(int[] array)
        {
            CopyTo(array, 0);
        }

        public void CopyTo(int[] array, int index)
        {
            if (array == null)
            {
                // Match the JIT's exception type here. For perf, a NullReference is thrown instead of an ArgumentNull.
                throw new NullReferenceException();
            }
            if (index < 0 || index >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if (array.Length - index < 2)
            {
                throw new ArgumentException();
            }
            array[index] = X;
            array[index + 1] = Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return obj is Vector2I asVector2I && Equals(asVector2I);
        }

        [JitIntrinsic]
        public bool Equals(Vector2I other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return ((X.GetHashCode() << 5) + X.GetHashCode()) ^ Y.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Length()
        {
            if (Vector.IsHardwareAccelerated)
            {
                var ls = Dot(this, this);
                return Convert.ToInt32(Math.Sqrt(ls));
            }
            else
            {
                var ls = X * X + Y * Y;
                return Convert.ToInt32(Math.Sqrt(ls));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int LengthSquared()
        {
            if (Vector.IsHardwareAccelerated) return Dot(this, this);
            return X * X + Y * Y;
        }

        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

            return $"<{X.ToString(format, formatProvider)}{separator} {Y.ToString(format, formatProvider)}>";
        }
    }
}