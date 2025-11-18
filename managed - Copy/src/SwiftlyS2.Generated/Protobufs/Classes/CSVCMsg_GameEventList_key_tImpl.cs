
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_GameEventList_key_tImpl : TypedProtobuf<CSVCMsg_GameEventList_key_t>, CSVCMsg_GameEventList_key_t
{
  public CSVCMsg_GameEventList_key_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Type
  { get => Accessor.GetInt32("type"); set => Accessor.SetInt32("type", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }

}
