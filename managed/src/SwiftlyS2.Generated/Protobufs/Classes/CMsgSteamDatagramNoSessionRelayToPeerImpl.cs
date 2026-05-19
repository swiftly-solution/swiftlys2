using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramNoSessionRelayToPeerImpl : TypedProtobuf<CMsgSteamDatagramNoSessionRelayToPeer>, CMsgSteamDatagramNoSessionRelayToPeer
{
    public CMsgSteamDatagramNoSessionRelayToPeerImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint LegacyRelaySessionId
    { get => Accessor.GetUInt32("legacy_relay_session_id"); set => Accessor.SetUInt32("legacy_relay_session_id", value); }
    public uint FromRelaySessionId
    { get => Accessor.GetUInt32("from_relay_session_id"); set => Accessor.SetUInt32("from_relay_session_id", value); }
    public uint FromConnectionId
    { get => Accessor.GetUInt32("from_connection_id"); set => Accessor.SetUInt32("from_connection_id", value); }
    public ulong KludgePad
    { get => Accessor.GetUInt64("kludge_pad"); set => Accessor.SetUInt64("kludge_pad", value); }
}