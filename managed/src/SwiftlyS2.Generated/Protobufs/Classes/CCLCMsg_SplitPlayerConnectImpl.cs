using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCLCMsg_SplitPlayerConnectImpl : NetMessage<CCLCMsg_SplitPlayerConnect>, CCLCMsg_SplitPlayerConnect
{
    public CCLCMsg_SplitPlayerConnectImpl( nint handle, bool isManuallyAllocated ) : base(handle, isManuallyAllocated)
    {
    }


    public string Playername { get => Accessor.GetString("playername"); set => Accessor.SetString("playername", value); }

}
