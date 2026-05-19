using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramRouterPingReply_AltAddress : ITypedProtobuf<CMsgSteamDatagramRouterPingReply_AltAddress>
{
    static CMsgSteamDatagramRouterPingReply_AltAddress ITypedProtobuf<CMsgSteamDatagramRouterPingReply_AltAddress>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramRouterPingReply_AltAddressImpl(handle, isManuallyAllocated);

    public uint Ipv4 { get; set; }
    public uint Port { get; set; }
    public uint Penalty { get; set; }
    public CMsgSteamDatagramRouterPingReply_AltAddress_Protocol Protocol { get; set; }
    public string Id { get; set; }
}