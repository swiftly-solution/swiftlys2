using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface QuantizedFloatEncoderAlias_t : ITypedProtobuf<QuantizedFloatEncoderAlias_t>
{
    static QuantizedFloatEncoderAlias_t ITypedProtobuf<QuantizedFloatEncoderAlias_t>.Wrap(nint handle, bool isManuallyAllocated) => new QuantizedFloatEncoderAlias_tImpl(handle, isManuallyAllocated);

    public string Name { get; set; }
    public int BitCount { get; set; }
    public int EncodeFlags { get; set; }
    public float MinValue { get; set; }
    public float MaxValue { get; set; }
    public bool Validate { get; set; }
}