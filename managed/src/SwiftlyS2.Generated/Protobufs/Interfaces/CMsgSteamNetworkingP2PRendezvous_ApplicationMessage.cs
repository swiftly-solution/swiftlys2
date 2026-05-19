using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamNetworkingP2PRendezvous_ApplicationMessage : ITypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ApplicationMessage>
{
    static CMsgSteamNetworkingP2PRendezvous_ApplicationMessage ITypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ApplicationMessage>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamNetworkingP2PRendezvous_ApplicationMessageImpl(handle, isManuallyAllocated);

    public byte[] Data { get; set; }
    public ulong MsgNum { get; set; }
    public uint Flags { get; set; }
    public uint LaneIdx { get; set; }
}