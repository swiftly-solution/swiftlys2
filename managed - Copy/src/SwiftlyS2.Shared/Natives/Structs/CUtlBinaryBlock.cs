using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CUtlBinaryBlock {

  private CUtlMemory<byte> _memory;
  private int _actualLength;


}