using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

/// <summary>
/// Radian Euler angle aligned to axis (NOT ROLL/PITCH/YAW)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4, Size = 12)]
public struct RadianEuler
{
  public float X;
  public float Y;
  public float Z;

  public RadianEuler(float x, float y, float z)
  {
    X = x;
    Y = y;
    Z = z;
  }

  public RadianEuler(RadianEuler other)
  {
    X = other.X;
    Y = other.Y;
    Z = other.Z;
  }

  public QAngle ToQAngle() => new(X * 180.0f / MathF.PI, Y * 180.0f / MathF.PI, Z * 180.0f / MathF.PI);

  public override bool Equals(object? obj) => obj is RadianEuler angle && this == angle;
  public override int GetHashCode() => HashCode.Combine(X, Y, Z);
  public override string ToString() => $"RadianEuler({X}, {Y}, {Z})";

  public static RadianEuler Zero => new(0, 0, 0);


  [MethodImpl(MethodImplOptions.AggressiveInlining)]

  public static RadianEuler operator +(RadianEuler a, RadianEuler b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static RadianEuler operator -(RadianEuler a, RadianEuler b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static RadianEuler operator *(RadianEuler a, RadianEuler b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static RadianEuler operator /(RadianEuler a, RadianEuler b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static RadianEuler operator *(RadianEuler a, float b) => new(a.X * b, a.Y * b, a.Z * b);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static RadianEuler operator /(RadianEuler a, float b)
  {
    if (b == 0)
    {
      throw new DivideByZeroException();
    }
    var oofl = 1.0f / b;
    return new(a.X * oofl, a.Y * oofl, a.Z * oofl);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static RadianEuler operator -(RadianEuler a) => new(-a.X, -a.Y, -a.Z);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(RadianEuler a, RadianEuler b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(RadianEuler a, RadianEuler b) => a.X != b.X || a.Y != b.Y || a.Z != b.Z;

}
