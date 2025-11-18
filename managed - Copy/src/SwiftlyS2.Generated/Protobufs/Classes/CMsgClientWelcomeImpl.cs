
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgClientWelcomeImpl : TypedProtobuf<CMsgClientWelcome>, CMsgClientWelcome
{
  public CMsgClientWelcomeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Version
  { get => Accessor.GetUInt32("version"); set => Accessor.SetUInt32("version", value); }


  public byte[] GameData
  { get => Accessor.GetBytes("game_data"); set => Accessor.SetBytes("game_data", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgSOCacheSubscribed> OutofdateSubscribedCaches
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgSOCacheSubscribed>(Accessor, "outofdate_subscribed_caches"); }


  public IProtobufRepeatedFieldSubMessageType<CMsgSOCacheSubscriptionCheck> UptodateSubscribedCaches
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgSOCacheSubscriptionCheck>(Accessor, "uptodate_subscribed_caches"); }


  public CMsgClientWelcome_Location Location
  { get => new CMsgClientWelcome_LocationImpl(NativeNetMessages.GetNestedMessage(Address, "location"), false); }


  public byte[] GameData2
  { get => Accessor.GetBytes("game_data2"); set => Accessor.SetBytes("game_data2", value); }


  public uint Rtime32GcWelcomeTimestamp
  { get => Accessor.GetUInt32("rtime32_gc_welcome_timestamp"); set => Accessor.SetUInt32("rtime32_gc_welcome_timestamp", value); }


  public uint Currency
  { get => Accessor.GetUInt32("currency"); set => Accessor.SetUInt32("currency", value); }


  public uint Balance
  { get => Accessor.GetUInt32("balance"); set => Accessor.SetUInt32("balance", value); }


  public string BalanceUrl
  { get => Accessor.GetString("balance_url"); set => Accessor.SetString("balance_url", value); }


  public string TxnCountryCode
  { get => Accessor.GetString("txn_country_code"); set => Accessor.SetString("txn_country_code", value); }

}
