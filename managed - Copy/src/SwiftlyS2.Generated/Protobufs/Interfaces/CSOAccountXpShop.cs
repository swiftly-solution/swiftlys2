
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOAccountXpShop : ITypedProtobuf<CSOAccountXpShop>
{
  static CSOAccountXpShop ITypedProtobuf<CSOAccountXpShop>.Wrap(nint handle, bool isManuallyAllocated) => new CSOAccountXpShopImpl(handle, isManuallyAllocated);


  public uint GenerationTime { get; set; }


  public uint RedeemableBalance { get; set; }


  public IProtobufRepeatedFieldValueType<uint> XpTracks { get; }

}
