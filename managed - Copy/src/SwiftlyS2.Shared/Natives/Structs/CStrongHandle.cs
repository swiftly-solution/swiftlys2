using System.Runtime.InteropServices;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Natives.NativeObjects;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.Natives;

/// <summary>
/// An partial implementation of the CStrongHandle struct.
/// </summary>
/// <typeparam name="T"></typeparam>
[StructLayout(LayoutKind.Sequential, Size = 8)]
public struct CStrongHandle<T> where T : INativeHandle, ISchemaClass<T> {

  private nint _pBinding;

  public readonly bool IsValid => _pBinding.IsValidPtr() && _pBinding.Read<nint>().IsValidPtr();

  public readonly T Value {
    get {
      if (!IsValid) {
        throw new InvalidOperationException("CStrongHandle is not valid.");
      }
      var handle = _pBinding.Read<nint>();

      return T.From(handle);
    }
  }
}
