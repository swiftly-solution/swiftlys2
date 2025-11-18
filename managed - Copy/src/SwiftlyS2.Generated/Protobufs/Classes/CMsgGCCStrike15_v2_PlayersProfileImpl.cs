
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_PlayersProfileImpl : TypedProtobuf<CMsgGCCStrike15_v2_PlayersProfile>, CMsgGCCStrike15_v2_PlayersProfile
{
  public CMsgGCCStrike15_v2_PlayersProfileImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint RequestId
  { get => Accessor.GetUInt32("request_id"); set => Accessor.SetUInt32("request_id", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_MatchmakingGC2ClientHello> AccountProfiles
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_MatchmakingGC2ClientHello>(Accessor, "account_profiles"); }

}
