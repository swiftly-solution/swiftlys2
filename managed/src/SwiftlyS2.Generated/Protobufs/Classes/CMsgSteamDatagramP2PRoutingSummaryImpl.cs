using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramP2PRoutingSummaryImpl : TypedProtobuf<CMsgSteamDatagramP2PRoutingSummary>, CMsgSteamDatagramP2PRoutingSummary
{
    public CMsgSteamDatagramP2PRoutingSummaryImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgSteamNetworkingICESessionSummary Ice
    { get => new CMsgSteamNetworkingICESessionSummaryImpl(NativeNetMessages.GetNestedMessage(Address, "ice"), false); }
    public CMsgSteamNetworkingP2PSDRRoutingSummary Sdr
    { get => new CMsgSteamNetworkingP2PSDRRoutingSummaryImpl(NativeNetMessages.GetNestedMessage(Address, "sdr"), false); }
}