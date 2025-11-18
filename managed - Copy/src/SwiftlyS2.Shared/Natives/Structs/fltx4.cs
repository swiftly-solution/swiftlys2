using System;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Explicit, Size = 16)]
public struct fltx4
{
  [FieldOffset(0)] public float F0;
  [FieldOffset(4)] public float F1;
  [FieldOffset(8)] public float F2;
  [FieldOffset(12)] public float F3;

  [FieldOffset(0)] public uint U0;
  [FieldOffset(4)] public uint U1;
  [FieldOffset(8)] public uint U2;
  [FieldOffset(12)] public uint U3;

  public float GetFloat(int index) => index switch
  {
    0 => F0,
    1 => F1,
    2 => F2,
    3 => F3,
    _ => throw new IndexOutOfRangeException()
  };

  public uint GetUInt(int index) => index switch
  {
    0 => U0,
    1 => U1,
    2 => U2,
    3 => U3,
    _ => throw new IndexOutOfRangeException()
  };

  public void SetFloat(int index, float value)
  {
    switch (index)
    {
      case 0: F0 = value; break;
      case 1: F1 = value; break;
      case 2: F2 = value; break;
      case 3: F3 = value; break;
      default: throw new IndexOutOfRangeException();
    }
  }

  public void SetUInt(int index, uint value)
  {
    switch (index)
    {
      case 0: U0 = value; break;
      case 1: U1 = value; break;
      case 2: U2 = value; break;
      case 3: U3 = value; break;
      default: throw new IndexOutOfRangeException();
    }
  }
}