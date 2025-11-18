
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgClearWorldDecalsEventImpl : NetMessage<CMsgClearWorldDecalsEvent>, CMsgClearWorldDecalsEvent
{
  public CMsgClearWorldDecalsEventImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Flagstoclear
  { get => Accessor.GetUInt32("flagstoclear"); set => Accessor.SetUInt32("flagstoclear", value); }

}
