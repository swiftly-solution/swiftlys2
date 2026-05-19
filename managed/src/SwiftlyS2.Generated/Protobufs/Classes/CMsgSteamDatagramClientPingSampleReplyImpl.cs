using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramClientPingSampleReplyImpl : TypedProtobuf<CMsgSteamDatagramClientPingSampleReply>, CMsgSteamDatagramClientPingSampleReply
{
    public CMsgSteamDatagramClientPingSampleReplyImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ConnectionId
    { get => Accessor.GetUInt32("connection_id"); set => Accessor.SetUInt32("connection_id", value); }
    public bool RelayOverrideActive
    { get => Accessor.GetBool("relay_override_active"); set => Accessor.SetBool("relay_override_active", value); }
    public CMsgTOSTreatment Tos
    { get => new CMsgTOSTreatmentImpl(NativeNetMessages.GetNestedMessage(Address, "tos"), false); }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramClientPingSampleReply_POP> Pops
    { get => new ProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramClientPingSampleReply_POP>(Accessor, "pops"); }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramClientPingSampleReply_LegacyDataCenter> LegacyDataCenters
    { get => new ProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramClientPingSampleReply_LegacyDataCenter>(Accessor, "legacy_data_centers"); }
}