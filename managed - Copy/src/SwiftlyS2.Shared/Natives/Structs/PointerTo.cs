using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.Natives;

/// <summary>
/// Pointer to a native handle.
/// </summary>
[StructLayout(LayoutKind.Sequential, Size = 8)]
public struct PointerTo<T> where T : INativeHandle, ISchemaClass<T> {
  private nint _pointer;

  public readonly T Value => T.From(_pointer);
}