using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamSockets_UDP_ConnectRequestImpl : TypedProtobuf<CMsgSteamSockets_UDP_ConnectRequest>, CMsgSteamSockets_UDP_ConnectRequest
{
    public CMsgSteamSockets_UDP_ConnectRequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ClientConnectionId
    { get => Accessor.GetUInt32("client_connection_id"); set => Accessor.SetUInt32("client_connection_id", value); }
    public ulong Challenge
    { get => Accessor.GetUInt64("challenge"); set => Accessor.SetUInt64("challenge", value); }
    public ulong MyTimestamp
    { get => Accessor.GetUInt64("my_timestamp"); set => Accessor.SetUInt64("my_timestamp", value); }
    public uint PingEstMs
    { get => Accessor.GetUInt32("ping_est_ms"); set => Accessor.SetUInt32("ping_est_ms", value); }
    public CMsgSteamDatagramSessionCryptInfoSigned Crypt
    { get => new CMsgSteamDatagramSessionCryptInfoSignedImpl(NativeNetMessages.GetNestedMessage(Address, "crypt"), false); }
    public CMsgSteamDatagramCertificateSigned Cert
    { get => new CMsgSteamDatagramCertificateSignedImpl(NativeNetMessages.GetNestedMessage(Address, "cert"), false); }
    public uint LegacyProtocolVersion
    { get => Accessor.GetUInt32("legacy_protocol_version"); set => Accessor.SetUInt32("legacy_protocol_version", value); }
    public string IdentityString
    { get => Accessor.GetString("identity_string"); set => Accessor.SetString("identity_string", value); }
    public ulong LegacyClientSteamId
    { get => Accessor.GetUInt64("legacy_client_steam_id"); set => Accessor.SetUInt64("legacy_client_steam_id", value); }
    public CMsgSteamNetworkingIdentityLegacyBinary LegacyIdentityBinary
    { get => new CMsgSteamNetworkingIdentityLegacyBinaryImpl(NativeNetMessages.GetNestedMessage(Address, "legacy_identity_binary"), false); }
}