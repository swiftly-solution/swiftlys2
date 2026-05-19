using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamNetworkingIdentityLegacyBinaryImpl : TypedProtobuf<CMsgSteamNetworkingIdentityLegacyBinary>, CMsgSteamNetworkingIdentityLegacyBinary
{
    public CMsgSteamNetworkingIdentityLegacyBinaryImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong SteamId
    { get => Accessor.GetUInt64("steam_id"); set => Accessor.SetUInt64("steam_id", value); }
    public byte[] GenericBytes
    { get => Accessor.GetBytes("generic_bytes"); set => Accessor.SetBytes("generic_bytes", value); }
    public string GenericString
    { get => Accessor.GetString("generic_string"); set => Accessor.SetString("generic_string", value); }
    public byte[] Ipv6AndPort
    { get => Accessor.GetBytes("ipv6_and_port"); set => Accessor.SetBytes("ipv6_and_port", value); }
}