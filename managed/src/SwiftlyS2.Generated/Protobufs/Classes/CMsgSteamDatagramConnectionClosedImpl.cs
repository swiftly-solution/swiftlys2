using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramConnectionClosedImpl : TypedProtobuf<CMsgSteamDatagramConnectionClosed>, CMsgSteamDatagramConnectionClosed
{
    public CMsgSteamDatagramConnectionClosedImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ToConnectionId
    { get => Accessor.GetUInt32("to_connection_id"); set => Accessor.SetUInt32("to_connection_id", value); }
    public uint FromConnectionId
    { get => Accessor.GetUInt32("from_connection_id"); set => Accessor.SetUInt32("from_connection_id", value); }
    public string FromIdentityString
    { get => Accessor.GetString("from_identity_string"); set => Accessor.SetString("from_identity_string", value); }
    public CMsgSteamNetworkingIdentityLegacyBinary LegacyFromIdentityBinary
    { get => new CMsgSteamNetworkingIdentityLegacyBinaryImpl(NativeNetMessages.GetNestedMessage(Address, "legacy_from_identity_binary"), false); }
    public ulong LegacyFromSteamId
    { get => Accessor.GetUInt64("legacy_from_steam_id"); set => Accessor.SetUInt64("legacy_from_steam_id", value); }
    public uint LegacyGameserverRelaySessionId
    { get => Accessor.GetUInt32("legacy_gameserver_relay_session_id"); set => Accessor.SetUInt32("legacy_gameserver_relay_session_id", value); }
    public uint ToRelaySessionId
    { get => Accessor.GetUInt32("to_relay_session_id"); set => Accessor.SetUInt32("to_relay_session_id", value); }
    public uint FromRelaySessionId
    { get => Accessor.GetUInt32("from_relay_session_id"); set => Accessor.SetUInt32("from_relay_session_id", value); }
    public byte[] ForwardTargetRelayRoutingToken
    { get => Accessor.GetBytes("forward_target_relay_routing_token"); set => Accessor.SetBytes("forward_target_relay_routing_token", value); }
    public uint ForwardTargetRevision
    { get => Accessor.GetUInt32("forward_target_revision"); set => Accessor.SetUInt32("forward_target_revision", value); }
    public CMsgSteamDatagramConnectionClosed_ERelayMode RelayMode
    { get => (CMsgSteamDatagramConnectionClosed_ERelayMode)Accessor.GetInt32("relay_mode"); set => Accessor.SetInt32("relay_mode", (int)value); }
    public string Debug
    { get => Accessor.GetString("debug"); set => Accessor.SetString("debug", value); }
    public uint ReasonCode
    { get => Accessor.GetUInt32("reason_code"); set => Accessor.SetUInt32("reason_code", value); }
    public ulong RoutingSecret
    { get => Accessor.GetUInt64("routing_secret"); set => Accessor.SetUInt64("routing_secret", value); }
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
}