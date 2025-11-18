
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_SurvivalStatsImpl : NetMessage<CCSUsrMsg_SurvivalStats>, CCSUsrMsg_SurvivalStats
{
  public CCSUsrMsg_SurvivalStatsImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public ulong Xuid
  { get => Accessor.GetUInt64("xuid"); set => Accessor.SetUInt64("xuid", value); }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_SurvivalStats_Fact> Facts
  { get => new ProtobufRepeatedFieldSubMessageType<CCSUsrMsg_SurvivalStats_Fact>(Accessor, "facts"); }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_SurvivalStats_Placement> Users
  { get => new ProtobufRepeatedFieldSubMessageType<CCSUsrMsg_SurvivalStats_Placement>(Accessor, "users"); }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_SurvivalStats_Damage> Damages
  { get => new ProtobufRepeatedFieldSubMessageType<CCSUsrMsg_SurvivalStats_Damage>(Accessor, "damages"); }


  public int Ticknumber
  { get => Accessor.GetInt32("ticknumber"); set => Accessor.SetInt32("ticknumber", value); }

}
