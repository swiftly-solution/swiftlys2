using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential, Size = 16)]
public struct Vector4D {
  public float X;
  public float Y;
  public float Z;
  public float W;

  public Vector4D(float x, float y, float z, float w) {
    X = x;
    Y = y;
    Z = z;
    W = w;
  }

  public Vector4D(Vector4D other) { 
    X = other.X;
    Y = other.Y;
    Z = other.Z;
    W = other.W;
  }

  public System.Numerics.Vector4 ToBuiltin() {
    return new(X, Y, Z, W);
  } 

  public static Vector4D FromBuiltin(System.Numerics.Vector4 vector) {
    return new(vector.X, vector.Y, vector.Z, vector.W);
  }

  public override bool Equals(object? obj) => obj is Vector4D vector && this == vector;
  public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
  public override string ToString() => $"Vector4D({X}, {Y}, {Z}, {W})";

  public static Vector4D Zero => new(0, 0, 0, 0);

  public static Vector4D One => new(1, 1, 1, 1);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static float Dot(Vector4D a, Vector4D b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public float Dot(Vector4D other) => Dot(this, other);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public float Length() => MathF.Sqrt(X * X + Y * Y + Z * Z + W * W);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public float LengthSquared() => X * X + Y * Y + Z * Z + W * W;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Normalize() {
    var len = Length();
    if (len > 0f) {
      var inv = 1.0f / len;
      X *= inv;
      Y *= inv;
      Z *= inv;
      W *= inv;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Vector4D Normalized() {
    var len = Length();
    if (len > 0f) {
      var inv = 1.0f / len;
      return new(X * inv, Y * inv, Z * inv, W * inv);
    }
    return Zero;
  }

  public void Deconstruct(out float x, out float y, out float z, out float w) {
    x = X;
    y = Y;
    z = Z;
    w = W;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Vector4D operator +(Vector4D a, Vector4D b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Vector4D operator -(Vector4D a, Vector4D b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]

  public static Vector4D operator *(Vector4D a, Vector4D b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]

  public static Vector4D operator /(Vector4D a, Vector4D b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]

  public static Vector4D operator *(Vector4D a, float b) => new(a.X * b, a.Y * b, a.Z * b, a.W * b);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]

  public static Vector4D operator *(float b, Vector4D a) => new(a.X * b, a.Y * b, a.Z * b, a.W * b);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]

  public static Vector4D operator /(Vector4D a, float b) {
    if (b == 0) {
      throw new DivideByZeroException();
    }
    var oofl = 1.0f / b;
    return new(a.X * oofl, a.Y * oofl, a.Z * oofl, a.W * oofl);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]

  public static Vector4D operator -(Vector4D a) => new(-a.X, -a.Y, -a.Z, -a.W);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]

  public static bool operator ==(Vector4D a, Vector4D b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(Vector4D a, Vector4D b) => a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.W != b.W;

}