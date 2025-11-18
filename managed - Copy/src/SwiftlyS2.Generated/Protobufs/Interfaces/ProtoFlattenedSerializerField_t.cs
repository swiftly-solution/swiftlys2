
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface ProtoFlattenedSerializerField_t : ITypedProtobuf<ProtoFlattenedSerializerField_t>
{
  static ProtoFlattenedSerializerField_t ITypedProtobuf<ProtoFlattenedSerializerField_t>.Wrap(nint handle, bool isManuallyAllocated) => new ProtoFlattenedSerializerField_tImpl(handle, isManuallyAllocated);


  public int VarTypeSym { get; set; }


  public int VarNameSym { get; set; }


  public int BitCount { get; set; }


  public float LowValue { get; set; }


  public float HighValue { get; set; }


  public int EncodeFlags { get; set; }


  public int FieldSerializerNameSym { get; set; }


  public int FieldSerializerVersion { get; set; }


  public int SendNodeSym { get; set; }


  public int VarEncoderSym { get; set; }


  public IProtobufRepeatedFieldSubMessageType<ProtoFlattenedSerializerField_t_polymorphic_field_t> PolymorphicTypes { get; }


  public int VarSerializerSym { get; set; }

}
