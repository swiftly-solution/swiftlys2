using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCErrorImpl : TypedProtobuf<CMsgGCError>, CMsgGCError
{
    public CMsgGCErrorImpl( nint handle, bool isManuallyAllocated ) : base(handle)
    {
    }


    public string ErrorText { get => Accessor.GetString("error_text"); set => Accessor.SetString("error_text", value); }

}
