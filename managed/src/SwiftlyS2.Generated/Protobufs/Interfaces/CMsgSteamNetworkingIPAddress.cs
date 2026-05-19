using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamNetworkingIPAddress : ITypedProtobuf<CMsgSteamNetworkingIPAddress>
{
    static CMsgSteamNetworkingIPAddress ITypedProtobuf<CMsgSteamNetworkingIPAddress>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamNetworkingIPAddressImpl(handle, isManuallyAllocated);

    public uint V4 { get; set; }
    public byte[] V6 { get; set; }
}