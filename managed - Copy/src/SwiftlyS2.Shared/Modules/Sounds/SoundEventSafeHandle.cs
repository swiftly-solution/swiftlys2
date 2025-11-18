using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.Sounds;

internal class SoundEventSafeHandle : AllocableNativeHandle {

  public SoundEventSafeHandle(nint handle) : base(handle, ownsHandle: true) {
  }

  protected override bool Free() {
    NativeSounds.DestroySoundEvent(Address);
    return true;
  }
}