using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

/// <summary>
/// 2-Dimensional vector for source 2.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 8)]
public struct Vector2D {
  public float X;
  public float Y;

  public Vector2D(float x, float y) {
    X = x;
    Y = y;
  }

  public Vector2D(Vector2D other) {
    X = other.X;
    Y = other.Y;
  }

  public System.Numerics.Vector2 ToBuiltin() {
    return new(X, Y);
  }

  public static Vector2D FromBuiltin(System.Numerics.Vector2 vector) {
    return new(vector.X, vector.Y);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public float Length() => (float)Math.Sqrt(X * X + Y * Y);


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public float LengthSquared() => X * X + Y * Y;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public float Distance(Vector2D other) => (this - other).Length();


  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public float DistanceSquared(Vector2D other) => (this - other).LengthSquared();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static float Dot(Vector2D a, Vector2D b) => a.X * b.X + a.Y * b.Y;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public float Dot(Vector2D other) => Dot(this, other);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void Normalize() {
    var len = Length();
    if (len > 0f) {
      var inv = 1.0f / len;
      X *= inv;
      Y *= inv;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Vector2D Normalized() {
    var len = Length();
    if (len > 0f) {
      var inv = 1.0f / len;
      return new(X * inv, Y * inv);
    }
    return Zero;
  }

  public void Deconstruct(out float x, out float y) {
    x = X;
    y = Y;
  }


  public override bool Equals(object? obj) => obj is Vector2D vector && this == vector;
  public override int GetHashCode() => HashCode.Combine(X, Y);
  public override string ToString() => $"Vector2D({X}, {Y})";

  public static Vector2D Zero => new(0, 0);

  public static Vector2D One => new(1, 1);


  [MethodImpl(MethodImplOptions.AggressiveInlining)]

  public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Vector2D operator -(Vector2D a, Vector2D b) => new(a.X - b.X, a.Y - b.Y);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Vector2D operator *(Vector2D a, Vector2D b) => new(a.X * b.X, a.Y * b.Y);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Vector2D operator /(Vector2D a, Vector2D b) => new(a.X / b.X, a.Y / b.Y);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Vector2D operator *(Vector2D a, float b) => new(a.X * b, a.Y * b);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Vector2D operator *(float b, Vector2D a) => new(a.X * b, a.Y * b);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Vector2D operator /(Vector2D a, float b) {
    if (b == 0) {
      throw new DivideByZeroException();
    }
    var oofl = 1.0f / b;
    return new(a.X * oofl, a.Y * oofl);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Vector2D operator -(Vector2D a) => new(-a.X, -a.Y);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(Vector2D a, Vector2D b) => a.X == b.X && a.Y == b.Y;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(Vector2D a, Vector2D b) => a.X != b.X || a.Y != b.Y;

}
