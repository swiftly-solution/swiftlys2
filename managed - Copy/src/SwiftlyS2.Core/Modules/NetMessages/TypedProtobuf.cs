using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Natives.NativeObjects;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Core.NetMessages;

internal class TypedProtobuf<T> : INativeHandle where T : ITypedProtobuf<T>
{

  public IProtobufAccessor Accessor { get; private init; }

  public TypedProtobuf(nint handle)
  {
    Accessor = new ProtobufAccessor(handle);
  }

  public nint Address => Accessor.Address;

  public bool IsValid => Accessor.IsValid;
}