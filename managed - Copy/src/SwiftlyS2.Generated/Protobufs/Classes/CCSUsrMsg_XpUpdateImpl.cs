
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_XpUpdateImpl : NetMessage<CCSUsrMsg_XpUpdate>, CCSUsrMsg_XpUpdate
{
  public CCSUsrMsg_XpUpdateImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public CMsgGCCstrike15_v2_GC2ServerNotifyXPRewarded Data
  { get => new CMsgGCCstrike15_v2_GC2ServerNotifyXPRewardedImpl(NativeNetMessages.GetNestedMessage(Address, "data"), false); }

}
