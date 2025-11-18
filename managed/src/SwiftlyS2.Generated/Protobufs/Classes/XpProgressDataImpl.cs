using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class XpProgressDataImpl : TypedProtobuf<XpProgressData>, XpProgressData
{
    public XpProgressDataImpl( nint handle, bool isManuallyAllocated ) : base(handle)
    {
    }


    public uint XpPoints { get => Accessor.GetUInt32("xp_points"); set => Accessor.SetUInt32("xp_points", value); }


    public int XpCategory { get => Accessor.GetInt32("xp_category"); set => Accessor.SetInt32("xp_category", value); }

}
