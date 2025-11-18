using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoStopImpl : TypedProtobuf<CDemoStop>, CDemoStop
{
    public CDemoStopImpl( nint handle, bool isManuallyAllocated ) : base(handle)
    {
    }


}
