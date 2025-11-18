
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCHRecurringSubscriptionStatusChangeImpl : TypedProtobuf<CMsgGCHRecurringSubscriptionStatusChange>, CMsgGCHRecurringSubscriptionStatusChange
{
  public CMsgGCHRecurringSubscriptionStatusChangeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Steamid
  { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }


  public uint Appid
  { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }


  public ulong Agreementid
  { get => Accessor.GetUInt64("agreementid"); set => Accessor.SetUInt64("agreementid", value); }


  public bool Active
  { get => Accessor.GetBool("active"); set => Accessor.SetBool("active", value); }

}
