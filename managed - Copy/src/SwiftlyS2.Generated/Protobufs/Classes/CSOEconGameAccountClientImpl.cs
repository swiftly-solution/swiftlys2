
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOEconGameAccountClientImpl : TypedProtobuf<CSOEconGameAccountClient>, CSOEconGameAccountClient
{
  public CSOEconGameAccountClientImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AdditionalBackpackSlots
  { get => Accessor.GetUInt32("additional_backpack_slots"); set => Accessor.SetUInt32("additional_backpack_slots", value); }


  public uint TradeBanExpiration
  { get => Accessor.GetUInt32("trade_ban_expiration"); set => Accessor.SetUInt32("trade_ban_expiration", value); }


  public uint BonusXpTimestampRefresh
  { get => Accessor.GetUInt32("bonus_xp_timestamp_refresh"); set => Accessor.SetUInt32("bonus_xp_timestamp_refresh", value); }


  public uint BonusXpUsedflags
  { get => Accessor.GetUInt32("bonus_xp_usedflags"); set => Accessor.SetUInt32("bonus_xp_usedflags", value); }


  public uint ElevatedState
  { get => Accessor.GetUInt32("elevated_state"); set => Accessor.SetUInt32("elevated_state", value); }


  public uint ElevatedTimestamp
  { get => Accessor.GetUInt32("elevated_timestamp"); set => Accessor.SetUInt32("elevated_timestamp", value); }

}
