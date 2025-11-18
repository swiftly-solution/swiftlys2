
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_FlattenedSerializer : ITypedProtobuf<CSVCMsg_FlattenedSerializer>, INetMessage<CSVCMsg_FlattenedSerializer>, IDisposable
{
  static int INetMessage<CSVCMsg_FlattenedSerializer>.MessageId => 41;
  
  static string INetMessage<CSVCMsg_FlattenedSerializer>.MessageName => "CSVCMsg_FlattenedSerializer";

  static CSVCMsg_FlattenedSerializer ITypedProtobuf<CSVCMsg_FlattenedSerializer>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_FlattenedSerializerImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<ProtoFlattenedSerializer_t> Serializers { get; }


  public IProtobufRepeatedFieldValueType<string> Symbols { get; }


  public IProtobufRepeatedFieldSubMessageType<ProtoFlattenedSerializerField_t> Fields { get; }

}
