using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class ProtoFlattenedSerializerField_t_proto_enum_info_tImpl : TypedProtobuf<ProtoFlattenedSerializerField_t_proto_enum_info_t>, ProtoFlattenedSerializerField_t_proto_enum_info_t
{
    public ProtoFlattenedSerializerField_t_proto_enum_info_tImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public bool IsSignedEnum
    { get => Accessor.GetBool("is_signed_enum"); set => Accessor.SetBool("is_signed_enum", value); }
}