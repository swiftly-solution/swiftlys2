
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_GameEventList_descriptor_tImpl : TypedProtobuf<CSVCMsg_GameEventList_descriptor_t>, CSVCMsg_GameEventList_descriptor_t
{
  public CSVCMsg_GameEventList_descriptor_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Eventid
  { get => Accessor.GetInt32("eventid"); set => Accessor.SetInt32("eventid", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public IProtobufRepeatedFieldSubMessageType<CSVCMsg_GameEventList_key_t> Keys
  { get => new ProtobufRepeatedFieldSubMessageType<CSVCMsg_GameEventList_key_t>(Accessor, "keys"); }

}
