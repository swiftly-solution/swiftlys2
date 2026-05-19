using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamNetworkingP2PRendezvous_ReliableMessageImpl : TypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ReliableMessage>, CMsgSteamNetworkingP2PRendezvous_ReliableMessage
{
    public CMsgSteamNetworkingP2PRendezvous_ReliableMessageImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgICERendezvous Ice
    { get => new CMsgICERendezvousImpl(NativeNetMessages.GetNestedMessage(Address, "ice"), false); }
}