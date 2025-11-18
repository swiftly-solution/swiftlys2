
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCLCMsg_BaselineAckImpl : NetMessage<CCLCMsg_BaselineAck>, CCLCMsg_BaselineAck
{
  public CCLCMsg_BaselineAckImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int BaselineTick
  { get => Accessor.GetInt32("baseline_tick"); set => Accessor.SetInt32("baseline_tick", value); }


  public int BaselineNr
  { get => Accessor.GetInt32("baseline_nr"); set => Accessor.SetInt32("baseline_nr", value); }

}
