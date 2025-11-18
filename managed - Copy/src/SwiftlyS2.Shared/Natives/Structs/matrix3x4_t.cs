using System.Runtime.InteropServices;
using SwiftlyS2.Core.Extensions;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Explicit, Size = 48)]
public struct matrix3x4_t {

  [FieldOffset(0x0)]
  private unsafe fixed float floats[12];


  public ref float this[int row, int column] {
    get {
      unsafe
      {
        if (row < 0 || row >= 3 || column < 0 || column >= 4)
          throw new IndexOutOfRangeException();
        return ref floats[row * 4 + column];
      }
    }
  }
} 