using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Explicit)]
public struct CTransform {

  [FieldOffset(0x0)]
  public Vector Position;

  [FieldOffset(0xC)]
  private int _pad001;

  [FieldOffset(0x10)]
  public Quaternion Orientation;
}