using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamNetworkingP2PRendezvous_ConnectionClosed : ITypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ConnectionClosed>
{
    static CMsgSteamNetworkingP2PRendezvous_ConnectionClosed ITypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ConnectionClosed>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamNetworkingP2PRendezvous_ConnectionClosedImpl(handle, isManuallyAllocated);

    public string Debug { get; set; }
    public uint ReasonCode { get; set; }
}