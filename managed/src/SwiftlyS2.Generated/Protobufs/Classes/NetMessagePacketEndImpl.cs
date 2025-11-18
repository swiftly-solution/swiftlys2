using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class NetMessagePacketEndImpl : TypedProtobuf<NetMessagePacketEnd>, NetMessagePacketEnd
{
    public NetMessagePacketEndImpl( nint handle, bool isManuallyAllocated ) : base(handle)
    {
    }


}
