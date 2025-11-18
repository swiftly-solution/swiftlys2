
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOAccountRecurringSubscriptionImpl : TypedProtobuf<CSOAccountRecurringSubscription>, CSOAccountRecurringSubscription
{
  public CSOAccountRecurringSubscriptionImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint TimeNextCycle
  { get => Accessor.GetUInt32("time_next_cycle"); set => Accessor.SetUInt32("time_next_cycle", value); }


  public uint TimeInitiated
  { get => Accessor.GetUInt32("time_initiated"); set => Accessor.SetUInt32("time_initiated", value); }

}
