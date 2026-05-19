using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramConnectionStatsClientToRouterImpl : TypedProtobuf<CMsgSteamDatagramConnectionStatsClientToRouter>, CMsgSteamDatagramConnectionStatsClientToRouter
{
    public CMsgSteamDatagramConnectionStatsClientToRouterImpl(nint handle, bool isManuallyAllocated) : base(handle)
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
    public uint ClientConnectionId
    { get => Accessor.GetUInt32("client_connection_id"); set => Accessor.SetUInt32("client_connection_id", value); }
    public uint SeqNumC2r
    { get => Accessor.GetUInt32("seq_num_c2r"); set => Accessor.SetUInt32("seq_num_c2r", value); }
    public uint SeqNumE2e
    { get => Accessor.GetUInt32("seq_num_e2e"); set => Accessor.SetUInt32("seq_num_e2e", value); }
}