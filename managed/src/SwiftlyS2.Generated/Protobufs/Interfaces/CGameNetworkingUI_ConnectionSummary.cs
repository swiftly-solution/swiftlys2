using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGameNetworkingUI_ConnectionSummary : ITypedProtobuf<CGameNetworkingUI_ConnectionSummary>
{
    static CGameNetworkingUI_ConnectionSummary ITypedProtobuf<CGameNetworkingUI_ConnectionSummary>.Wrap(nint handle, bool isManuallyAllocated) => new CGameNetworkingUI_ConnectionSummaryImpl(handle, isManuallyAllocated);

    public uint TransportKind { get; set; }
    public uint ConnectionState { get; set; }
    public string SdrpopLocal { get; set; }
    public string SdrpopRemote { get; set; }
    public uint PingMs { get; set; }
    public float PacketLoss { get; set; }
    public uint PingDefaultInternetRoute { get; set; }
    public bool IpWasShared { get; set; }
}