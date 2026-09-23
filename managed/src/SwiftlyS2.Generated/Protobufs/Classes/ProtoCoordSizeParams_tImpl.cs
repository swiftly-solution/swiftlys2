using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class ProtoCoordSizeParams_tImpl : TypedProtobuf<ProtoCoordSizeParams_t>, ProtoCoordSizeParams_t
{
    public ProtoCoordSizeParams_tImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public int CoordIntegerBits
    { get => Accessor.GetInt32("coord_integer_bits"); set => Accessor.SetInt32("coord_integer_bits", value); }
    public int CoordFractionalBits
    { get => Accessor.GetInt32("coord_fractional_bits"); set => Accessor.SetInt32("coord_fractional_bits", value); }
    public int CoordIntegerBitsMp
    { get => Accessor.GetInt32("coord_integer_bits_mp"); set => Accessor.SetInt32("coord_integer_bits_mp", value); }
    public int CoordFractionalBitsMp
    { get => Accessor.GetInt32("coord_fractional_bits_mp"); set => Accessor.SetInt32("coord_fractional_bits_mp", value); }
    public int NormalFractionalBits
    { get => Accessor.GetInt32("normal_fractional_bits"); set => Accessor.SetInt32("normal_fractional_bits", value); }
    public int AngleBits
    { get => Accessor.GetInt32("angle_bits"); set => Accessor.SetInt32("angle_bits", value); }
}