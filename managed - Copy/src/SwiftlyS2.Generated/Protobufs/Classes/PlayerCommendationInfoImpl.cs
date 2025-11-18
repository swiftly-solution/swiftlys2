
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class PlayerCommendationInfoImpl : TypedProtobuf<PlayerCommendationInfo>, PlayerCommendationInfo
{
  public PlayerCommendationInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint CmdFriendly
  { get => Accessor.GetUInt32("cmd_friendly"); set => Accessor.SetUInt32("cmd_friendly", value); }


  public uint CmdTeaching
  { get => Accessor.GetUInt32("cmd_teaching"); set => Accessor.SetUInt32("cmd_teaching", value); }


  public uint CmdLeader
  { get => Accessor.GetUInt32("cmd_leader"); set => Accessor.SetUInt32("cmd_leader", value); }

}
