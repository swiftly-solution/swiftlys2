using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoSendTablesImpl : TypedProtobuf<CDemoSendTables>, CDemoSendTables
{
    public CDemoSendTablesImpl( nint handle, bool isManuallyAllocated ) : base(handle)
    {
    }


    public byte[] Data { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }

}
