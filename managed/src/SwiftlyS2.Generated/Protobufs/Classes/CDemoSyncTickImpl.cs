using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoSyncTickImpl : TypedProtobuf<CDemoSyncTick>, CDemoSyncTick
{
    public CDemoSyncTickImpl( nint handle, bool isManuallyAllocated ) : base(handle)
    {
    }


}
