using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgMultiImpl : TypedProtobuf<CMsgMulti>, CMsgMulti
{
    public CMsgMultiImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint SizeUnzipped
    { get => Accessor.GetUInt32("size_unzipped"); set => Accessor.SetUInt32("size_unzipped", value); }
    public byte[] MessageBody
    { get => Accessor.GetBytes("message_body"); set => Accessor.SetBytes("message_body", value); }
}