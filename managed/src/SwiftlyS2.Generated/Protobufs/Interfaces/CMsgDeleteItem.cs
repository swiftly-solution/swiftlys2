using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgDeleteItem : ITypedProtobuf<CMsgDeleteItem>
{
    static CMsgDeleteItem ITypedProtobuf<CMsgDeleteItem>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgDeleteItemImpl(handle, isManuallyAllocated);

    public ulong ItemId { get; set; }
}