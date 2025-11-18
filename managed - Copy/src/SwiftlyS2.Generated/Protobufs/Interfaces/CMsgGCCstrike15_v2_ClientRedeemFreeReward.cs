
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCstrike15_v2_ClientRedeemFreeReward : ITypedProtobuf<CMsgGCCstrike15_v2_ClientRedeemFreeReward>
{
  static CMsgGCCstrike15_v2_ClientRedeemFreeReward ITypedProtobuf<CMsgGCCstrike15_v2_ClientRedeemFreeReward>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCstrike15_v2_ClientRedeemFreeRewardImpl(handle, isManuallyAllocated);


  public uint GenerationTime { get; set; }


  public uint RedeemableBalance { get; set; }


  public IProtobufRepeatedFieldValueType<ulong> Items { get; }

}
