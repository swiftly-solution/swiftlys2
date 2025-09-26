
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOEconGameAccountClient : ITypedProtobuf<CSOEconGameAccountClient>
{
  static CSOEconGameAccountClient ITypedProtobuf<CSOEconGameAccountClient>.Wrap(nint handle, bool isManuallyAllocated) => new CSOEconGameAccountClientImpl(handle, isManuallyAllocated);


  public uint AdditionalBackpackSlots { get; set; }


  public uint TradeBanExpiration { get; set; }


  public uint BonusXpTimestampRefresh { get; set; }


  public uint BonusXpUsedflags { get; set; }


  public uint ElevatedState { get; set; }


  public uint ElevatedTimestamp { get; set; }

}
