using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamNetworkingP2PRendezvous_ConnectionClosedImpl : TypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ConnectionClosed>, CMsgSteamNetworkingP2PRendezvous_ConnectionClosed
{
    public CMsgSteamNetworkingP2PRendezvous_ConnectionClosedImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string Debug
    { get => Accessor.GetString("debug"); set => Accessor.SetString("debug", value); }
    public uint ReasonCode
    { get => Accessor.GetUInt32("reason_code"); set => Accessor.SetUInt32("reason_code", value); }
}