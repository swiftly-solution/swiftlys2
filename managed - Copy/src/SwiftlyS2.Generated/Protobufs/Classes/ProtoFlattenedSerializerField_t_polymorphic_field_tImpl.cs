
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class ProtoFlattenedSerializerField_t_polymorphic_field_tImpl : TypedProtobuf<ProtoFlattenedSerializerField_t_polymorphic_field_t>, ProtoFlattenedSerializerField_t_polymorphic_field_t
{
  public ProtoFlattenedSerializerField_t_polymorphic_field_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int PolymorphicFieldSerializerNameSym
  { get => Accessor.GetInt32("polymorphic_field_serializer_name_sym"); set => Accessor.SetInt32("polymorphic_field_serializer_name_sym", value); }


  public int PolymorphicFieldSerializerVersion
  { get => Accessor.GetInt32("polymorphic_field_serializer_version"); set => Accessor.SetInt32("polymorphic_field_serializer_version", value); }

}
