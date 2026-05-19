using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramConnectionStatsRouterToServerImpl : TypedProtobuf<CMsgSteamDatagramConnectionStatsRouterToServer>, CMsgSteamDatagramConnectionStatsRouterToServer
{
    public CMsgSteamDatagramConnectionStatsRouterToServerImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgSteamDatagramConnectionQuality QualityRelay
    { get => new CMsgSteamDatagramConnectionQualityImpl(NativeNetMessages.GetNestedMessage(Address, "quality_relay"), false); }
    public CMsgSteamDatagramConnectionQuality QualityE2e
    { get => new CMsgSteamDatagramConnectionQualityImpl(NativeNetMessages.GetNestedMessage(Address, "quality_e2e"), false); }
    public IProtobufRepeatedFieldValueType<uint> AckRelay
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "ack_relay"); }
    public IProtobufRepeatedFieldValueType<uint> LegacyAckE2e
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "legacy_ack_e2e"); }
    public uint Flags
    { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }
    public uint SeqNumR2s
    { get => Accessor.GetUInt32("seq_num_r2s"); set => Accessor.SetUInt32("seq_num_r2s", value); }
    public uint SeqNumE2e
    { get => Accessor.GetUInt32("seq_num_e2e"); set => Accessor.SetUInt32("seq_num_e2e", value); }
    public string ClientIdentityString
    { get => Accessor.GetString("client_identity_string"); set => Accessor.SetString("client_identity_string", value); }
    public ulong LegacyClientSteamId
    { get => Accessor.GetUInt64("legacy_client_steam_id"); set => Accessor.SetUInt64("legacy_client_steam_id", value); }
    public uint RelaySessionId
    { get => Accessor.GetUInt32("relay_session_id"); set => Accessor.SetUInt32("relay_session_id", value); }
    public uint ClientConnectionId
    { get => Accessor.GetUInt32("client_connection_id"); set => Accessor.SetUInt32("client_connection_id", value); }
    public uint ServerConnectionId
    { get => Accessor.GetUInt32("server_connection_id"); set => Accessor.SetUInt32("server_connection_id", value); }
    public ulong RoutingSecret
    { get => Accessor.GetUInt64("routing_secret"); set => Accessor.SetUInt64("routing_secret", value); }
}