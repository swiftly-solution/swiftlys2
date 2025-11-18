
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_StopSoundImpl : NetMessage<CSVCMsg_StopSound>, CSVCMsg_StopSound
{
  public CSVCMsg_StopSoundImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Guid
  { get => Accessor.GetUInt32("guid"); set => Accessor.SetUInt32("guid", value); }

}
