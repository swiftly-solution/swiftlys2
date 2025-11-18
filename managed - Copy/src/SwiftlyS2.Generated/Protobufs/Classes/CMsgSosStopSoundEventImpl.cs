
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSosStopSoundEventImpl : NetMessage<CMsgSosStopSoundEvent>, CMsgSosStopSoundEvent
{
  public CMsgSosStopSoundEventImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int SoundeventGuid
  { get => Accessor.GetInt32("soundevent_guid"); set => Accessor.SetInt32("soundevent_guid", value); }

}
