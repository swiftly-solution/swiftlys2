using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGameNetworkingUI_AppSummary : ITypedProtobuf<CGameNetworkingUI_AppSummary>
{
    static CGameNetworkingUI_AppSummary ITypedProtobuf<CGameNetworkingUI_AppSummary>.Wrap(nint handle, bool isManuallyAllocated) => new CGameNetworkingUI_AppSummaryImpl(handle, isManuallyAllocated);

    public uint Appid { get; set; }
    public bool IpWasSharedWithFriend { get; set; }
    public bool IpWasSharedWithNonfriend { get; set; }
    public uint ActiveConnections { get; set; }
    public CGameNetworkingUI_ConnectionSummary MainCxn { get; }
}