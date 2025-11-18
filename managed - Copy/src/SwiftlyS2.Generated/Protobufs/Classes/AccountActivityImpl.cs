
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class AccountActivityImpl : TypedProtobuf<AccountActivity>, AccountActivity
{
  public AccountActivityImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Activity
  { get => Accessor.GetUInt32("activity"); set => Accessor.SetUInt32("activity", value); }


  public uint Mode
  { get => Accessor.GetUInt32("mode"); set => Accessor.SetUInt32("mode", value); }


  public uint Map
  { get => Accessor.GetUInt32("map"); set => Accessor.SetUInt32("map", value); }


  public ulong Matchid
  { get => Accessor.GetUInt64("matchid"); set => Accessor.SetUInt64("matchid", value); }

}
