
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgClientWelcome : ITypedProtobuf<CMsgClientWelcome>
{
  static CMsgClientWelcome ITypedProtobuf<CMsgClientWelcome>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgClientWelcomeImpl(handle, isManuallyAllocated);


  public uint Version { get; set; }


  public byte[] GameData { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgSOCacheSubscribed> OutofdateSubscribedCaches { get; }


  public IProtobufRepeatedFieldSubMessageType<CMsgSOCacheSubscriptionCheck> UptodateSubscribedCaches { get; }


  public CMsgClientWelcome_Location Location { get; }


  public byte[] GameData2 { get; set; }


  public uint Rtime32GcWelcomeTimestamp { get; set; }


  public uint Currency { get; set; }


  public uint Balance { get; set; }


  public string BalanceUrl { get; set; }


  public string TxnCountryCode { get; set; }

}
