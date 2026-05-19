using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGameNetworkingUI_MessageImpl : TypedProtobuf<CGameNetworkingUI_Message>, CGameNetworkingUI_Message
{
    public CGameNetworkingUI_MessageImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public IProtobufRepeatedFieldSubMessageType<CGameNetworkingUI_ConnectionState> ConnectionState
    { get => new ProtobufRepeatedFieldSubMessageType<CGameNetworkingUI_ConnectionState>(Accessor, "connection_state"); }
}