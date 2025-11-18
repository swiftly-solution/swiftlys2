
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOAccountItemPersonalStore : ITypedProtobuf<CSOAccountItemPersonalStore>
{
  static CSOAccountItemPersonalStore ITypedProtobuf<CSOAccountItemPersonalStore>.Wrap(nint handle, bool isManuallyAllocated) => new CSOAccountItemPersonalStoreImpl(handle, isManuallyAllocated);


  public uint GenerationTime { get; set; }


  public uint RedeemableBalance { get; set; }


  public IProtobufRepeatedFieldValueType<ulong> Items { get; }

}
