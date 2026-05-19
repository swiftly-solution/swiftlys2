using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgProtobufWrappedImpl : TypedProtobuf<CMsgProtobufWrapped>, CMsgProtobufWrapped
{
    public CMsgProtobufWrappedImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public byte[] MessageBody
    { get => Accessor.GetBytes("message_body"); set => Accessor.SetBytes("message_body", value); }
}