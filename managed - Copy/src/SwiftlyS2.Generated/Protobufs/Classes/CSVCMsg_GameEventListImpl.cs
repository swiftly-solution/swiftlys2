
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_GameEventListImpl : TypedProtobuf<CSVCMsg_GameEventList>, CSVCMsg_GameEventList
{
  public CSVCMsg_GameEventListImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CSVCMsg_GameEventList_descriptor_t> Descriptors
  { get => new ProtobufRepeatedFieldSubMessageType<CSVCMsg_GameEventList_descriptor_t>(Accessor, "descriptors"); }

}
