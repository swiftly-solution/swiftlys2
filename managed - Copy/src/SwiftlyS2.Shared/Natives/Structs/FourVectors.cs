using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Explicit, Size = 48, Pack = 16)]
public struct FourVectors {

  [FieldOffset(0x0)]
  public fltx4 X;

  [FieldOffset(0x10)]
  public fltx4 Y;

  [FieldOffset(0x20)]
  public fltx4 Z;

  public Vector GetVector(int index) {
    return new(X.GetFloat(index), Y.GetFloat(index), Z.GetFloat(index));
  }

  public void SetVector(int index, Vector vector) {
    X.SetFloat(index, vector.X);
    Y.SetFloat(index, vector.Y);
    Z.SetFloat(index, vector.Z);
  }

  public Vector this[int index] {
    get {
      return index switch {
        0 => GetVector(0),
        1 => GetVector(1),
        2 => GetVector(2),
        3 => GetVector(3),
        _ => throw new IndexOutOfRangeException()
      };
    }
    set {
      SetVector(index, value);
    }
  }
}