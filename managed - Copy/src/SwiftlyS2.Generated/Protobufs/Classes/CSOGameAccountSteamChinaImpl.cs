
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOGameAccountSteamChinaImpl : TypedProtobuf<CSOGameAccountSteamChina>, CSOGameAccountSteamChina
{
  public CSOGameAccountSteamChinaImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint TimeLastUpdate
  { get => Accessor.GetUInt32("time_last_update"); set => Accessor.SetUInt32("time_last_update", value); }


  public uint TimeCommsBan
  { get => Accessor.GetUInt32("time_comms_ban"); set => Accessor.SetUInt32("time_comms_ban", value); }


  public uint TimePlayBan
  { get => Accessor.GetUInt32("time_play_ban"); set => Accessor.SetUInt32("time_play_ban", value); }

}
