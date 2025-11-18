
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class ProtoFlattenedSerializer_tImpl : TypedProtobuf<ProtoFlattenedSerializer_t>, ProtoFlattenedSerializer_t
{
  public ProtoFlattenedSerializer_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int SerializerNameSym
  { get => Accessor.GetInt32("serializer_name_sym"); set => Accessor.SetInt32("serializer_name_sym", value); }


  public int SerializerVersion
  { get => Accessor.GetInt32("serializer_version"); set => Accessor.SetInt32("serializer_version", value); }


  public IProtobufRepeatedFieldValueType<int> FieldsIndex
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "fields_index"); }

}
