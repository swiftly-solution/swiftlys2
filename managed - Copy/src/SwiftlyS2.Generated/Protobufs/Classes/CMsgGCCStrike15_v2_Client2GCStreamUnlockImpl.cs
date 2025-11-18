
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Client2GCStreamUnlockImpl : TypedProtobuf<CMsgGCCStrike15_v2_Client2GCStreamUnlock>, CMsgGCCStrike15_v2_Client2GCStreamUnlock
{
  public CMsgGCCStrike15_v2_Client2GCStreamUnlockImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Ticket
  { get => Accessor.GetUInt64("ticket"); set => Accessor.SetUInt64("ticket", value); }


  public int Os
  { get => Accessor.GetInt32("os"); set => Accessor.SetInt32("os", value); }

}
