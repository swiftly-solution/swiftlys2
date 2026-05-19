using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgMulti : ITypedProtobuf<CMsgMulti>
{
    static CMsgMulti ITypedProtobuf<CMsgMulti>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgMultiImpl(handle, isManuallyAllocated);

    public uint SizeUnzipped { get; set; }
    public byte[] MessageBody { get; set; }
}