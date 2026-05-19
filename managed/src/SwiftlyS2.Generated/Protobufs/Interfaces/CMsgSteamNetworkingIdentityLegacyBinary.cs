using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamNetworkingIdentityLegacyBinary : ITypedProtobuf<CMsgSteamNetworkingIdentityLegacyBinary>
{
    static CMsgSteamNetworkingIdentityLegacyBinary ITypedProtobuf<CMsgSteamNetworkingIdentityLegacyBinary>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamNetworkingIdentityLegacyBinaryImpl(handle, isManuallyAllocated);

    public ulong SteamId { get; set; }
    public byte[] GenericBytes { get; set; }
    public string GenericString { get; set; }
    public byte[] Ipv6AndPort { get; set; }
}