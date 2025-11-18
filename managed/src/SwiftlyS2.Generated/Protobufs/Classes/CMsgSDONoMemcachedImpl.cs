using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSDONoMemcachedImpl : TypedProtobuf<CMsgSDONoMemcached>, CMsgSDONoMemcached
{
    public CMsgSDONoMemcachedImpl( nint handle, bool isManuallyAllocated ) : base(handle)
    {
    }


}
