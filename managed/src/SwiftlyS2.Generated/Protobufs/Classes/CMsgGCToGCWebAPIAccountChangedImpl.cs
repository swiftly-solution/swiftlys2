using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCToGCWebAPIAccountChangedImpl : TypedProtobuf<CMsgGCToGCWebAPIAccountChanged>, CMsgGCToGCWebAPIAccountChanged
{
    public CMsgGCToGCWebAPIAccountChangedImpl( nint handle, bool isManuallyAllocated ) : base(handle)
    {
    }


}
