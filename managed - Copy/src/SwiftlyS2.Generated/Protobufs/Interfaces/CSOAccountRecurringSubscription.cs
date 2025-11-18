
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOAccountRecurringSubscription : ITypedProtobuf<CSOAccountRecurringSubscription>
{
  static CSOAccountRecurringSubscription ITypedProtobuf<CSOAccountRecurringSubscription>.Wrap(nint handle, bool isManuallyAllocated) => new CSOAccountRecurringSubscriptionImpl(handle, isManuallyAllocated);


  public uint TimeNextCycle { get; set; }


  public uint TimeInitiated { get; set; }

}
