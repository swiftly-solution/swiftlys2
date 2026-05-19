using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramRelayAuthTicket_ExtraFieldImpl : TypedProtobuf<CMsgSteamDatagramRelayAuthTicket_ExtraField>, CMsgSteamDatagramRelayAuthTicket_ExtraField
{
    public CMsgSteamDatagramRelayAuthTicket_ExtraFieldImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string Name
    { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }
    public string StringValue
    { get => Accessor.GetString("string_value"); set => Accessor.SetString("string_value", value); }
    public long Int64Value
    { get => Accessor.GetInt64("int64_value"); set => Accessor.SetInt64("int64_value", value); }
    public ulong Fixed64Value
    { get => Accessor.GetUInt64("fixed64_value"); set => Accessor.SetUInt64("fixed64_value", value); }
}