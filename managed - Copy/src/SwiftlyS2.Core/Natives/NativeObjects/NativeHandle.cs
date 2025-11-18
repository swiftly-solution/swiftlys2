using System.Numerics;
using System.Runtime.CompilerServices;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives.NativeObjects;

internal class NativeHandle : INativeHandle {
  protected nint _Handle { get; set; }

  public bool IsValid { get => _Handle != nint.Zero; }

  public NativeHandle(nint handle) {
    _Handle = handle;
  }

  public nint Address => _Handle;
}