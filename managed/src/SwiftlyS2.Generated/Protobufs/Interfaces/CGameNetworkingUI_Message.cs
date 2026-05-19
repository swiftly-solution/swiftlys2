using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGameNetworkingUI_Message : ITypedProtobuf<CGameNetworkingUI_Message>
{
    static CGameNetworkingUI_Message ITypedProtobuf<CGameNetworkingUI_Message>.Wrap(nint handle, bool isManuallyAllocated) => new CGameNetworkingUI_MessageImpl(handle, isManuallyAllocated);

    public IProtobufRepeatedFieldSubMessageType<CGameNetworkingUI_ConnectionState> ConnectionState { get; }
}