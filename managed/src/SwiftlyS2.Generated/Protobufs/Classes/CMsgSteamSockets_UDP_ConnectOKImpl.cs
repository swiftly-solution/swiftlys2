using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamSockets_UDP_ConnectOKImpl : TypedProtobuf<CMsgSteamSockets_UDP_ConnectOK>, CMsgSteamSockets_UDP_ConnectOK
{
    public CMsgSteamSockets_UDP_ConnectOKImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ClientConnectionId
    { get => Accessor.GetUInt32("client_connection_id"); set => Accessor.SetUInt32("client_connection_id", value); }
    public uint ServerConnectionId
    { get => Accessor.GetUInt32("server_connection_id"); set => Accessor.SetUInt32("server_connection_id", value); }
    public ulong YourTimestamp
    { get => Accessor.GetUInt64("your_timestamp"); set => Accessor.SetUInt64("your_timestamp", value); }
    public uint DelayTimeUsec
    { get => Accessor.GetUInt32("delay_time_usec"); set => Accessor.SetUInt32("delay_time_usec", value); }
    public CMsgSteamDatagramSessionCryptInfoSigned Crypt
    { get => new CMsgSteamDatagramSessionCryptInfoSignedImpl(NativeNetMessages.GetNestedMessage(Address, "crypt"), false); }
    public CMsgSteamDatagramCertificateSigned Cert
    { get => new CMsgSteamDatagramCertificateSignedImpl(NativeNetMessages.GetNestedMessage(Address, "cert"), false); }
    public string IdentityString
    { get => Accessor.GetString("identity_string"); set => Accessor.SetString("identity_string", value); }
    public ulong LegacyServerSteamId
    { get => Accessor.GetUInt64("legacy_server_steam_id"); set => Accessor.SetUInt64("legacy_server_steam_id", value); }
    public CMsgSteamNetworkingIdentityLegacyBinary LegacyIdentityBinary
    { get => new CMsgSteamNetworkingIdentityLegacyBinaryImpl(NativeNetMessages.GetNestedMessage(Address, "legacy_identity_binary"), false); }
}