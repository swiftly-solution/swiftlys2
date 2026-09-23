using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface ProtoFlattenedSerializerField_t_proto_enum_info_t : ITypedProtobuf<ProtoFlattenedSerializerField_t_proto_enum_info_t>
{
    static ProtoFlattenedSerializerField_t_proto_enum_info_t ITypedProtobuf<ProtoFlattenedSerializerField_t_proto_enum_info_t>.Wrap(nint handle, bool isManuallyAllocated) => new ProtoFlattenedSerializerField_t_proto_enum_info_tImpl(handle, isManuallyAllocated);

    public bool IsSignedEnum { get; set; }
}