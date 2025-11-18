
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface ProtoFlattenedSerializerField_t_polymorphic_field_t : ITypedProtobuf<ProtoFlattenedSerializerField_t_polymorphic_field_t>
{
  static ProtoFlattenedSerializerField_t_polymorphic_field_t ITypedProtobuf<ProtoFlattenedSerializerField_t_polymorphic_field_t>.Wrap(nint handle, bool isManuallyAllocated) => new ProtoFlattenedSerializerField_t_polymorphic_field_tImpl(handle, isManuallyAllocated);


  public int PolymorphicFieldSerializerNameSym { get; set; }


  public int PolymorphicFieldSerializerVersion { get; set; }

}
