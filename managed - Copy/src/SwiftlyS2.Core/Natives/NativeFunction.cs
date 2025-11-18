using System.Runtime.InteropServices;

namespace SwiftlyS2.Core.Natives;

[StructLayout(LayoutKind.Sequential, Pack = 16, Size = 16)]
internal unsafe struct NativeFunction {
  public nint Name;
  public nint Function;
}