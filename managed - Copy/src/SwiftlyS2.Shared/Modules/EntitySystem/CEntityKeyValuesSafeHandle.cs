using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.EntitySystem;

internal class CEntityKeyValuesSafeHandle : AllocableNativeHandle {

  public CEntityKeyValuesSafeHandle(nint handle) : base(handle, ownsHandle: true) {
  }

  protected override bool Free() {
    NativeCEntityKeyValues.Deallocate(Address);
    return true;
  }
}