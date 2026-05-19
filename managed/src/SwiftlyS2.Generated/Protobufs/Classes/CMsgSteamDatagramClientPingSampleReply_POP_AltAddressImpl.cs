using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramClientPingSampleReply_POP_AltAddressImpl : TypedProtobuf<CMsgSteamDatagramClientPingSampleReply_POP_AltAddress>, CMsgSteamDatagramClientPingSampleReply_POP_AltAddress
{
    public CMsgSteamDatagramClientPingSampleReply_POP_AltAddressImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string Id
    { get => Accessor.GetString("id"); set => Accessor.SetString("id", value); }
    public uint FrontPingMs
    { get => Accessor.GetUInt32("front_ping_ms"); set => Accessor.SetUInt32("front_ping_ms", value); }
    public uint Penalty
    { get => Accessor.GetUInt32("penalty"); set => Accessor.SetUInt32("penalty", value); }
}