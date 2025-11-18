
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface ProtoFlattenedSerializer_t : ITypedProtobuf<ProtoFlattenedSerializer_t>
{
  static ProtoFlattenedSerializer_t ITypedProtobuf<ProtoFlattenedSerializer_t>.Wrap(nint handle, bool isManuallyAllocated) => new ProtoFlattenedSerializer_tImpl(handle, isManuallyAllocated);


  public int SerializerNameSym { get; set; }


  public int SerializerVersion { get; set; }


  public IProtobufRepeatedFieldValueType<int> FieldsIndex { get; }

}
