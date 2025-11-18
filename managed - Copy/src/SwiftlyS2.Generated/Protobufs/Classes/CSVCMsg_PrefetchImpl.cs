
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_PrefetchImpl : NetMessage<CSVCMsg_Prefetch>, CSVCMsg_Prefetch
{
  public CSVCMsg_PrefetchImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int SoundIndex
  { get => Accessor.GetInt32("sound_index"); set => Accessor.SetInt32("sound_index", value); }


  public PrefetchType ResourceType
  { get => (PrefetchType)Accessor.GetInt32("resource_type"); set => Accessor.SetInt32("resource_type", (int)value); }

}
