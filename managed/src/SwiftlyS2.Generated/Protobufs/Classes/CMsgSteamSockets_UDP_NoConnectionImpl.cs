using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamSockets_UDP_NoConnectionImpl : TypedProtobuf<CMsgSteamSockets_UDP_NoConnection>, CMsgSteamSockets_UDP_NoConnection
{
    public CMsgSteamSockets_UDP_NoConnectionImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint FromConnectionId
    { get => Accessor.GetUInt32("from_connection_id"); set => Accessor.SetUInt32("from_connection_id", value); }
    public uint ToConnectionId
    { get => Accessor.GetUInt32("to_connection_id"); set => Accessor.SetUInt32("to_connection_id", value); }
}