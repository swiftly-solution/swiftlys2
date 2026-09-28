using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class QuantizedFloatEncoderAlias_tImpl : TypedProtobuf<QuantizedFloatEncoderAlias_t>, QuantizedFloatEncoderAlias_t
{
    public QuantizedFloatEncoderAlias_tImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string Name
    { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }
    public int BitCount
    { get => Accessor.GetInt32("bit_count"); set => Accessor.SetInt32("bit_count", value); }
    public int EncodeFlags
    { get => Accessor.GetInt32("encode_flags"); set => Accessor.SetInt32("encode_flags", value); }
    public float MinValue
    { get => Accessor.GetFloat("min_value"); set => Accessor.SetFloat("min_value", value); }
    public float MaxValue
    { get => Accessor.GetFloat("max_value"); set => Accessor.SetFloat("max_value", value); }
    public bool Validate
    { get => Accessor.GetBool("validate"); set => Accessor.SetBool("validate", value); }
}