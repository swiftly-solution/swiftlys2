
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientCommendPlayerImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientCommendPlayer>, CMsgGCCStrike15_v2_ClientCommendPlayer
{
  public CMsgGCCStrike15_v2_ClientCommendPlayerImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public ulong MatchId
  { get => Accessor.GetUInt64("match_id"); set => Accessor.SetUInt64("match_id", value); }


  public PlayerCommendationInfo Commendation
  { get => new PlayerCommendationInfoImpl(NativeNetMessages.GetNestedMessage(Address, "commendation"), false); }


  public uint Tokens
  { get => Accessor.GetUInt32("tokens"); set => Accessor.SetUInt32("tokens", value); }

}
