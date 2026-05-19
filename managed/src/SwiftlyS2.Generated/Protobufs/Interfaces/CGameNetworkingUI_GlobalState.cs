using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGameNetworkingUI_GlobalState : ITypedProtobuf<CGameNetworkingUI_GlobalState>
{
    static CGameNetworkingUI_GlobalState ITypedProtobuf<CGameNetworkingUI_GlobalState>.Wrap(nint handle, bool isManuallyAllocated) => new CGameNetworkingUI_GlobalStateImpl(handle, isManuallyAllocated);

}