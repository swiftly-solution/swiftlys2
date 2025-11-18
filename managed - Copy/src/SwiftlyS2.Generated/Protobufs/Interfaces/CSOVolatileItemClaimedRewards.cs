
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOVolatileItemClaimedRewards : ITypedProtobuf<CSOVolatileItemClaimedRewards>
{
  static CSOVolatileItemClaimedRewards ITypedProtobuf<CSOVolatileItemClaimedRewards>.Wrap(nint handle, bool isManuallyAllocated) => new CSOVolatileItemClaimedRewardsImpl(handle, isManuallyAllocated);


  public uint Defidx { get; set; }


  public IProtobufRepeatedFieldValueType<uint> Reward { get; }


  public IProtobufRepeatedFieldValueType<uint> GenerationTime { get; }

}
