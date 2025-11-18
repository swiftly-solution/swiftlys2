
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSosStopSoundEventHashImpl : NetMessage<CMsgSosStopSoundEventHash>, CMsgSosStopSoundEventHash
{
  public CMsgSosStopSoundEventHashImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint SoundeventHash
  { get => Accessor.GetUInt32("soundevent_hash"); set => Accessor.SetUInt32("soundevent_hash", value); }


  public int SourceEntityIndex
  { get => Accessor.GetInt32("source_entity_index"); set => Accessor.SetInt32("source_entity_index", value); }

}
