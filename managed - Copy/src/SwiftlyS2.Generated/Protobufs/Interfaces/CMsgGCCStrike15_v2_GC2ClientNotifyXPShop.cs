
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_GC2ClientNotifyXPShop : ITypedProtobuf<CMsgGCCStrike15_v2_GC2ClientNotifyXPShop>
{
  static CMsgGCCStrike15_v2_GC2ClientNotifyXPShop ITypedProtobuf<CMsgGCCStrike15_v2_GC2ClientNotifyXPShop>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_GC2ClientNotifyXPShopImpl(handle, isManuallyAllocated);


  public CSOAccountXpShop Prematch { get; }


  public CSOAccountXpShop Postmatch { get; }


  public uint CurrentXp { get; set; }


  public uint CurrentLevel { get; set; }

}
