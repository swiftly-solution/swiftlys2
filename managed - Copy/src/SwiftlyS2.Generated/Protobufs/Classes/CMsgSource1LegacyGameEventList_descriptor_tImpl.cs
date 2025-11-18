
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSource1LegacyGameEventList_descriptor_tImpl : TypedProtobuf<CMsgSource1LegacyGameEventList_descriptor_t>, CMsgSource1LegacyGameEventList_descriptor_t
{
  public CMsgSource1LegacyGameEventList_descriptor_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Eventid
  { get => Accessor.GetInt32("eventid"); set => Accessor.SetInt32("eventid", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgSource1LegacyGameEventList_key_t> Keys
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgSource1LegacyGameEventList_key_t>(Accessor, "keys"); }

}
