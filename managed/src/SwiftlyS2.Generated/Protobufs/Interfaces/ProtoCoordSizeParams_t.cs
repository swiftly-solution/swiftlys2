using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface ProtoCoordSizeParams_t : ITypedProtobuf<ProtoCoordSizeParams_t>
{
    static ProtoCoordSizeParams_t ITypedProtobuf<ProtoCoordSizeParams_t>.Wrap(nint handle, bool isManuallyAllocated) => new ProtoCoordSizeParams_tImpl(handle, isManuallyAllocated);

    public int CoordIntegerBits { get; set; }
    public int CoordFractionalBits { get; set; }
    public int CoordIntegerBitsMp { get; set; }
    public int CoordFractionalBitsMp { get; set; }
    public int NormalFractionalBits { get; set; }
    public int AngleBits { get; set; }
}