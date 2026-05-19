using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramNoConnectionImpl : TypedProtobuf<CMsgSteamDatagramNoConnection>, CMsgSteamDatagramNoConnection
{
    public CMsgSteamDatagramNoConnectionImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ToConnectionId
    { get => Accessor.GetUInt32("to_connection_id"); set => Accessor.SetUInt32("to_connection_id", value); }
    public uint FromConnectionId
    { get => Accessor.GetUInt32("from_connection_id"); set => Accessor.SetUInt32("from_connection_id", value); }
    public uint LegacyGameserverRelaySessionId
    { get => Accessor.GetUInt32("legacy_gameserver_relay_session_id"); set => Accessor.SetUInt32("legacy_gameserver_relay_session_id", value); }
    public uint ToRelaySessionId
    { get => Accessor.GetUInt32("to_relay_session_id"); set => Accessor.SetUInt32("to_relay_session_id", value); }
    public uint FromRelaySessionId
    { get => Accessor.GetUInt32("from_relay_session_id"); set => Accessor.SetUInt32("from_relay_session_id", value); }
    public string FromIdentityString
    { get => Accessor.GetString("from_identity_string"); set => Accessor.SetString("from_identity_string", value); }
    public ulong LegacyFromSteamId
    { get => Accessor.GetUInt64("legacy_from_steam_id"); set => Accessor.SetUInt64("legacy_from_steam_id", value); }
    public bool EndToEnd
    { get => Accessor.GetBool("end_to_end"); set => Accessor.SetBool("end_to_end", value); }
    public bool NotPrimarySession
    { get => Accessor.GetBool("not_primary_session"); set => Accessor.SetBool("not_primary_session", value); }
    public bool NotPrimaryTransport
    { get => Accessor.GetBool("not_primary_transport"); set => Accessor.SetBool("not_primary_transport", value); }
    public bool RelayOverrideActive
    { get => Accessor.GetBool("relay_override_active"); set => Accessor.SetBool("relay_override_active", value); }
    public CMsgSteamDatagramConnectionQuality QualityRelay
    { get => new CMsgSteamDatagramConnectionQualityImpl(NativeNetMessages.GetNestedMessage(Address, "quality_relay"), false); }
    public CMsgSteamDatagramConnectionQuality QualityE2e
    { get => new CMsgSteamDatagramConnectionQualityImpl(NativeNetMessages.GetNestedMessage(Address, "quality_e2e"), false); }
    public CMsgSteamDatagramP2PRoutingSummary P2pRoutingSummary
    { get => new CMsgSteamDatagramP2PRoutingSummaryImpl(NativeNetMessages.GetNestedMessage(Address, "p2p_routing_summary"), false); }
    public ulong RoutingSecret
    { get => Accessor.GetUInt64("routing_secret"); set => Accessor.SetUInt64("routing_secret", value); }
    public uint DummyPad
    { get => Accessor.GetUInt32("dummy_pad"); set => Accessor.SetUInt32("dummy_pad", value); }
}