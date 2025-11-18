
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOQuestProgressImpl : TypedProtobuf<CSOQuestProgress>, CSOQuestProgress
{
  public CSOQuestProgressImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Questid
  { get => Accessor.GetUInt32("questid"); set => Accessor.SetUInt32("questid", value); }


  public uint PointsRemaining
  { get => Accessor.GetUInt32("points_remaining"); set => Accessor.SetUInt32("points_remaining", value); }


  public uint BonusPoints
  { get => Accessor.GetUInt32("bonus_points"); set => Accessor.SetUInt32("bonus_points", value); }

}
