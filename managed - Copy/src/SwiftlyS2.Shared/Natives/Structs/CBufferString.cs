using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential, Size = 16)]
public struct CBufferString {
  private unsafe fixed byte _dummy[16];

  public unsafe void todo() {
    fixed (void* ptr = &this) {
      // TODO: Implement some methods with this ptr
    }
  }
}