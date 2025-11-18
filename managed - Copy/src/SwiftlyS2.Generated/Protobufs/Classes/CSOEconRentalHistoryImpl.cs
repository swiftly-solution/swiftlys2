
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOEconRentalHistoryImpl : TypedProtobuf<CSOEconRentalHistory>, CSOEconRentalHistory
{
  public CSOEconRentalHistoryImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public ulong CrateItemId
  { get => Accessor.GetUInt64("crate_item_id"); set => Accessor.SetUInt64("crate_item_id", value); }


  public uint CrateDefIndex
  { get => Accessor.GetUInt32("crate_def_index"); set => Accessor.SetUInt32("crate_def_index", value); }


  public uint IssueDate
  { get => Accessor.GetUInt32("issue_date"); set => Accessor.SetUInt32("issue_date", value); }


  public uint ExpirationDate
  { get => Accessor.GetUInt32("expiration_date"); set => Accessor.SetUInt32("expiration_date", value); }

}
