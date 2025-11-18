
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_FlattenedSerializerImpl : NetMessage<CSVCMsg_FlattenedSerializer>, CSVCMsg_FlattenedSerializer
{
  public CSVCMsg_FlattenedSerializerImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<ProtoFlattenedSerializer_t> Serializers
  { get => new ProtobufRepeatedFieldSubMessageType<ProtoFlattenedSerializer_t>(Accessor, "serializers"); }


  public IProtobufRepeatedFieldValueType<string> Symbols
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "symbols"); }


  public IProtobufRepeatedFieldSubMessageType<ProtoFlattenedSerializerField_t> Fields
  { get => new ProtobufRepeatedFieldSubMessageType<ProtoFlattenedSerializerField_t>(Accessor, "fields"); }

}
