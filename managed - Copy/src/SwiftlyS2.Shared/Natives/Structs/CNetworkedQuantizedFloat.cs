using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct CNetworkedQuantizedFloat {

  [FieldOffset(0x0)]
  public float Value;

  [FieldOffset(0x4)]
  public ushort Encoder;

  [FieldOffset(0x7)]
  public bool Unflattened;
}
