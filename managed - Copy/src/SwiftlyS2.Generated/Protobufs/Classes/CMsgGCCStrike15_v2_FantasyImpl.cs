
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_FantasyImpl : TypedProtobuf<CMsgGCCStrike15_v2_Fantasy>, CMsgGCCStrike15_v2_Fantasy
{
  public CMsgGCCStrike15_v2_FantasyImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint EventId
  { get => Accessor.GetUInt32("event_id"); set => Accessor.SetUInt32("event_id", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_Fantasy_FantasyTeam> Teams
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_Fantasy_FantasyTeam>(Accessor, "teams"); }

}
