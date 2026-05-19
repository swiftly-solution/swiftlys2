using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamNetworkingP2PRendezvous_ReliableMessage : ITypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ReliableMessage>
{
    static CMsgSteamNetworkingP2PRendezvous_ReliableMessage ITypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ReliableMessage>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamNetworkingP2PRendezvous_ReliableMessageImpl(handle, isManuallyAllocated);

    public CMsgICERendezvous Ice { get; }
}