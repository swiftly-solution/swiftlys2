using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGameNetworkingUI_GlobalStateImpl : TypedProtobuf<CGameNetworkingUI_GlobalState>, CGameNetworkingUI_GlobalState
{
    public CGameNetworkingUI_GlobalStateImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

}