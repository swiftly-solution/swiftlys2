using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamSockets_UDP_ConnectionClosedImpl : TypedProtobuf<CMsgSteamSockets_UDP_ConnectionClosed>, CMsgSteamSockets_UDP_ConnectionClosed
{
    public CMsgSteamSockets_UDP_ConnectionClosedImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ToConnectionId
    { get => Accessor.GetUInt32("to_connection_id"); set => Accessor.SetUInt32("to_connection_id", value); }
    public uint FromConnectionId
    { get => Accessor.GetUInt32("from_connection_id"); set => Accessor.SetUInt32("from_connection_id", value); }
    public string Debug
    { get => Accessor.GetString("debug"); set => Accessor.SetString("debug", value); }
    public uint ReasonCode
    { get => Accessor.GetUInt32("reason_code"); set => Accessor.SetUInt32("reason_code", value); }
}