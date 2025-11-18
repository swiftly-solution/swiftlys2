
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_GC2ClientNotifyXPShopImpl : TypedProtobuf<CMsgGCCStrike15_v2_GC2ClientNotifyXPShop>, CMsgGCCStrike15_v2_GC2ClientNotifyXPShop
{
  public CMsgGCCStrike15_v2_GC2ClientNotifyXPShopImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public CSOAccountXpShop Prematch
  { get => new CSOAccountXpShopImpl(NativeNetMessages.GetNestedMessage(Address, "prematch"), false); }


  public CSOAccountXpShop Postmatch
  { get => new CSOAccountXpShopImpl(NativeNetMessages.GetNestedMessage(Address, "postmatch"), false); }


  public uint CurrentXp
  { get => Accessor.GetUInt32("current_xp"); set => Accessor.SetUInt32("current_xp", value); }


  public uint CurrentLevel
  { get => Accessor.GetUInt32("current_level"); set => Accessor.SetUInt32("current_level", value); }

}
