using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgIPAddressImpl : TypedProtobuf<CMsgIPAddress>, CMsgIPAddress
{
    public CMsgIPAddressImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint V4
    { get => Accessor.GetUInt32("v4"); set => Accessor.SetUInt32("v4", value); }
    public byte[] V6
    { get => Accessor.GetBytes("v6"); set => Accessor.SetBytes("v6", value); }
}