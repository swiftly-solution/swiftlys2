
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCHRecurringSubscriptionStatusChange : ITypedProtobuf<CMsgGCHRecurringSubscriptionStatusChange>
{
  static CMsgGCHRecurringSubscriptionStatusChange ITypedProtobuf<CMsgGCHRecurringSubscriptionStatusChange>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCHRecurringSubscriptionStatusChangeImpl(handle, isManuallyAllocated);


  public ulong Steamid { get; set; }


  public uint Appid { get; set; }


  public ulong Agreementid { get; set; }


  public bool Active { get; set; }

}
