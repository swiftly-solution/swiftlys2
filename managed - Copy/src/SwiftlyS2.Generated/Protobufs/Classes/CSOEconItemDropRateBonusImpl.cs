
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOEconItemDropRateBonusImpl : TypedProtobuf<CSOEconItemDropRateBonus>, CSOEconItemDropRateBonus
{
  public CSOEconItemDropRateBonusImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint ExpirationDate
  { get => Accessor.GetUInt32("expiration_date"); set => Accessor.SetUInt32("expiration_date", value); }


  public float Bonus
  { get => Accessor.GetFloat("bonus"); set => Accessor.SetFloat("bonus", value); }


  public uint BonusCount
  { get => Accessor.GetUInt32("bonus_count"); set => Accessor.SetUInt32("bonus_count", value); }


  public ulong ItemId
  { get => Accessor.GetUInt64("item_id"); set => Accessor.SetUInt64("item_id", value); }


  public uint DefIndex
  { get => Accessor.GetUInt32("def_index"); set => Accessor.SetUInt32("def_index", value); }

}
