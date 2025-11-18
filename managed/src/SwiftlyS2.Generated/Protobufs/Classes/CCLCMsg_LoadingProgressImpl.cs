using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCLCMsg_LoadingProgressImpl : NetMessage<CCLCMsg_LoadingProgress>, CCLCMsg_LoadingProgress
{
    public CCLCMsg_LoadingProgressImpl( nint handle, bool isManuallyAllocated ) : base(handle, isManuallyAllocated)
    {
    }


    public int Progress { get => Accessor.GetInt32("progress"); set => Accessor.SetInt32("progress", value); }

}
