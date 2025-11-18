using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.NetMessages;

internal class NetMessageAllocableNativeHandle : AllocableNativeHandle {

  public NetMessageAllocableNativeHandle(nint handle) : base(handle, true) {
  }

  protected override bool Free() {
    NativeNetMessages.DeallocateNetMessage(Address);
    return true;
  }

}