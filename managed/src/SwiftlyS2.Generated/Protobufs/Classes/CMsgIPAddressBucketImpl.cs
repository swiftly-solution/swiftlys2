using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgIPAddressBucketImpl : TypedProtobuf<CMsgIPAddressBucket>, CMsgIPAddressBucket
{
    public CMsgIPAddressBucketImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgIPAddress OriginalIpAddress
    { get => new CMsgIPAddressImpl(NativeNetMessages.GetNestedMessage(Address, "original_ip_address"), false); }
    public ulong Bucket
    { get => Accessor.GetUInt64("bucket"); set => Accessor.SetUInt64("bucket", value); }
}