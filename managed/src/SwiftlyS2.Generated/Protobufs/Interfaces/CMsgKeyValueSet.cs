using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgKeyValueSet : ITypedProtobuf<CMsgKeyValueSet>
{
    static CMsgKeyValueSet ITypedProtobuf<CMsgKeyValueSet>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgKeyValueSetImpl(handle, isManuallyAllocated);

    public IProtobufRepeatedFieldSubMessageType<CMsgKeyValuePair> Pairs { get; }
}