
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class ProtoFlattenedSerializerField_tImpl : TypedProtobuf<ProtoFlattenedSerializerField_t>, ProtoFlattenedSerializerField_t
{
  public ProtoFlattenedSerializerField_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int VarTypeSym
  { get => Accessor.GetInt32("var_type_sym"); set => Accessor.SetInt32("var_type_sym", value); }


  public int VarNameSym
  { get => Accessor.GetInt32("var_name_sym"); set => Accessor.SetInt32("var_name_sym", value); }


  public int BitCount
  { get => Accessor.GetInt32("bit_count"); set => Accessor.SetInt32("bit_count", value); }


  public float LowValue
  { get => Accessor.GetFloat("low_value"); set => Accessor.SetFloat("low_value", value); }


  public float HighValue
  { get => Accessor.GetFloat("high_value"); set => Accessor.SetFloat("high_value", value); }


  public int EncodeFlags
  { get => Accessor.GetInt32("encode_flags"); set => Accessor.SetInt32("encode_flags", value); }


  public int FieldSerializerNameSym
  { get => Accessor.GetInt32("field_serializer_name_sym"); set => Accessor.SetInt32("field_serializer_name_sym", value); }


  public int FieldSerializerVersion
  { get => Accessor.GetInt32("field_serializer_version"); set => Accessor.SetInt32("field_serializer_version", value); }


  public int SendNodeSym
  { get => Accessor.GetInt32("send_node_sym"); set => Accessor.SetInt32("send_node_sym", value); }


  public int VarEncoderSym
  { get => Accessor.GetInt32("var_encoder_sym"); set => Accessor.SetInt32("var_encoder_sym", value); }


  public IProtobufRepeatedFieldSubMessageType<ProtoFlattenedSerializerField_t_polymorphic_field_t> PolymorphicTypes
  { get => new ProtobufRepeatedFieldSubMessageType<ProtoFlattenedSerializerField_t_polymorphic_field_t>(Accessor, "polymorphic_types"); }


  public int VarSerializerSym
  { get => Accessor.GetInt32("var_serializer_sym"); set => Accessor.SetInt32("var_serializer_sym", value); }

}
