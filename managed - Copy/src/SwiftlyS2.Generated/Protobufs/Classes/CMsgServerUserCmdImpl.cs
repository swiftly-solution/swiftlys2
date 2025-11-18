
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgServerUserCmdImpl : TypedProtobuf<CMsgServerUserCmd>, CMsgServerUserCmd
{
  public CMsgServerUserCmdImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public byte[] Data
  { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }


  public int CmdNumber
  { get => Accessor.GetInt32("cmd_number"); set => Accessor.SetInt32("cmd_number", value); }


  public int PlayerSlot
  { get => Accessor.GetInt32("player_slot"); set => Accessor.SetInt32("player_slot", value); }


  public int ServerTickExecuted
  { get => Accessor.GetInt32("server_tick_executed"); set => Accessor.SetInt32("server_tick_executed", value); }


  public int ClientTick
  { get => Accessor.GetInt32("client_tick"); set => Accessor.SetInt32("client_tick", value); }

}
