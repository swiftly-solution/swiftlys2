using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramConnectRequestImpl : TypedProtobuf<CMsgSteamDatagramConnectRequest>, CMsgSteamDatagramConnectRequest
{
    public CMsgSteamDatagramConnectRequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ConnectionId
    { get => Accessor.GetUInt32("connection_id"); set => Accessor.SetUInt32("connection_id", value); }
    public ulong MyTimestamp
    { get => Accessor.GetUInt64("my_timestamp"); set => Accessor.SetUInt64("my_timestamp", value); }
    public uint PingEstMs
    { get => Accessor.GetUInt32("ping_est_ms"); set => Accessor.SetUInt32("ping_est_ms", value); }
    public uint VirtualPort
    { get => Accessor.GetUInt32("virtual_port"); set => Accessor.SetUInt32("virtual_port", value); }
    public uint GameserverRelaySessionId
    { get => Accessor.GetUInt32("gameserver_relay_session_id"); set => Accessor.SetUInt32("gameserver_relay_session_id", value); }
    public CMsgSteamDatagramSessionCryptInfoSigned Crypt
    { get => new CMsgSteamDatagramSessionCryptInfoSignedImpl(NativeNetMessages.GetNestedMessage(Address, "crypt"), false); }
    public CMsgSteamDatagramCertificateSigned Cert
    { get => new CMsgSteamDatagramCertificateSignedImpl(NativeNetMessages.GetNestedMessage(Address, "cert"), false); }
    public ulong RoutingSecret
    { get => Accessor.GetUInt64("routing_secret"); set => Accessor.SetUInt64("routing_secret", value); }
    public ulong LegacyClientSteamId
    { get => Accessor.GetUInt64("legacy_client_steam_id"); set => Accessor.SetUInt64("legacy_client_steam_id", value); }
}