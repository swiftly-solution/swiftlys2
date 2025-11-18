using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTERadioIconImpl : TypedProtobuf<CMsgTERadioIcon>, CMsgTERadioIcon
{
    public CMsgTERadioIconImpl( nint handle, bool isManuallyAllocated ) : base(handle)
    {
    }


    public uint Player { get => Accessor.GetUInt32("player"); set => Accessor.SetUInt32("player", value); }

}
