
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgCStrike15WelcomeImpl : TypedProtobuf<CMsgCStrike15Welcome>, CMsgCStrike15Welcome
{
  public CMsgCStrike15WelcomeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint StoreItemHash
  { get => Accessor.GetUInt32("store_item_hash"); set => Accessor.SetUInt32("store_item_hash", value); }


  public uint Timeplayedconsecutively
  { get => Accessor.GetUInt32("timeplayedconsecutively"); set => Accessor.SetUInt32("timeplayedconsecutively", value); }


  public uint TimeFirstPlayed
  { get => Accessor.GetUInt32("time_first_played"); set => Accessor.SetUInt32("time_first_played", value); }


  public uint LastTimePlayed
  { get => Accessor.GetUInt32("last_time_played"); set => Accessor.SetUInt32("last_time_played", value); }


  public uint LastIpAddress
  { get => Accessor.GetUInt32("last_ip_address"); set => Accessor.SetUInt32("last_ip_address", value); }


  public ulong Gscookieid
  { get => Accessor.GetUInt64("gscookieid"); set => Accessor.SetUInt64("gscookieid", value); }


  public ulong Uniqueid
  { get => Accessor.GetUInt64("uniqueid"); set => Accessor.SetUInt64("uniqueid", value); }

}
