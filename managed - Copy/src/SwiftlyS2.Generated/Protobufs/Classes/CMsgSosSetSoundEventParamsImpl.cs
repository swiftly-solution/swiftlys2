
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSosSetSoundEventParamsImpl : NetMessage<CMsgSosSetSoundEventParams>, CMsgSosSetSoundEventParams
{
  public CMsgSosSetSoundEventParamsImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int SoundeventGuid
  { get => Accessor.GetInt32("soundevent_guid"); set => Accessor.SetInt32("soundevent_guid", value); }


  public byte[] PackedParams
  { get => Accessor.GetBytes("packed_params"); set => Accessor.SetBytes("packed_params", value); }

}
