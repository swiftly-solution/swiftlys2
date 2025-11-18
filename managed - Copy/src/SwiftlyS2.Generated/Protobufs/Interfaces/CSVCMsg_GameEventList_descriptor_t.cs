
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_GameEventList_descriptor_t : ITypedProtobuf<CSVCMsg_GameEventList_descriptor_t>
{
  static CSVCMsg_GameEventList_descriptor_t ITypedProtobuf<CSVCMsg_GameEventList_descriptor_t>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_GameEventList_descriptor_tImpl(handle, isManuallyAllocated);


  public int Eventid { get; set; }


  public string Name { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CSVCMsg_GameEventList_key_t> Keys { get; }

}
