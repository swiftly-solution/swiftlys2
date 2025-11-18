using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

/// <summary>
/// Quaternion, basically 4 floats.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 16, Size = 16)]
public struct Quaternion
{
  public float X;
  public float Y;
  public float Z;
  public float W;

  public Quaternion(float ix, float iy, float iz, float iw)
  {
    X = ix;
    Y = iy;
    Z = iz;
    W = iw;
  }

  public Quaternion(Quaternion other)
  {
    X = other.X;
    Y = other.Y;
    Z = other.Z;
    W = other.W;
  }

  public System.Numerics.Quaternion ToBuiltin()
  {
    return new(X, Y, Z, W);
  }

  public static Quaternion FromBuiltin(System.Numerics.Quaternion quaternion)
  {
    return new(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
  }

  public override bool Equals(object? obj) => obj is Quaternion quaternion && this == quaternion;
  public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
  public override string ToString() => $"Quaternion({X}, {Y}, {Z}, {W})";

  public static Quaternion Zero => new(0, 0, 0, 0);

  public static Quaternion One => new(1, 1, 1, 1);


  [MethodImpl(MethodImplOptions.AggressiveInlining)]

  public static Quaternion operator +(Quaternion a, Quaternion b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Quaternion operator -(Quaternion a, Quaternion b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Quaternion operator /(Quaternion a, Quaternion b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Quaternion operator *(Quaternion a, float b) => new(a.X * b, a.Y * b, a.Z * b, a.W * b);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Quaternion operator /(Quaternion a, float b)
  {
    if (b == 0)
    {
      throw new DivideByZeroException();
    }
    var oofl = 1.0f / b;
    return new(a.X * oofl, a.Y * oofl, a.Z * oofl, a.W * oofl);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Quaternion operator -(Quaternion a) => new(-a.X, -a.Y, -a.Z, -a.W);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(Quaternion a, Quaternion b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(Quaternion a, Quaternion b) => a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.W != b.W;

}
