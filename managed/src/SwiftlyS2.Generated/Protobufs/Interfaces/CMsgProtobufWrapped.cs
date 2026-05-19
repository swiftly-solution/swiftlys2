using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgProtobufWrapped : ITypedProtobuf<CMsgProtobufWrapped>
{
    static CMsgProtobufWrapped ITypedProtobuf<CMsgProtobufWrapped>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgProtobufWrappedImpl(handle, isManuallyAllocated);

    public byte[] MessageBody { get; set; }
}