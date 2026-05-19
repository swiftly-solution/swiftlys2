using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgKeyValuePair : ITypedProtobuf<CMsgKeyValuePair>
{
    static CMsgKeyValuePair ITypedProtobuf<CMsgKeyValuePair>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgKeyValuePairImpl(handle, isManuallyAllocated);

    public string Name { get; set; }
    public string Value { get; set; }
}