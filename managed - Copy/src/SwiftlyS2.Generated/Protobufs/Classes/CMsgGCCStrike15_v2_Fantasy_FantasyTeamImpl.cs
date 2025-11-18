
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Fantasy_FantasyTeamImpl : TypedProtobuf<CMsgGCCStrike15_v2_Fantasy_FantasyTeam>, CMsgGCCStrike15_v2_Fantasy_FantasyTeam
{
  public CMsgGCCStrike15_v2_Fantasy_FantasyTeamImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Sectionid
  { get => Accessor.GetInt32("sectionid"); set => Accessor.SetInt32("sectionid", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_Fantasy_FantasySlot> Slots
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_Fantasy_FantasySlot>(Accessor, "slots"); }

}
