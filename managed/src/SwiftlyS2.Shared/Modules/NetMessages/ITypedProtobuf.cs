using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.NetMessages;

public interface ITypedProtobuf<T> : INativeHandle where T : ITypedProtobuf<T>
{
    public IProtobufAccessor Accessor { get; }
    internal static abstract T Wrap( nint handle, bool isManuallyAllocated );
}