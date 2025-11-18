
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientToGCChatImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientToGCChat>, CMsgGCCStrike15_v2_ClientToGCChat
{
  public CMsgGCCStrike15_v2_ClientToGCChatImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong MatchId
  { get => Accessor.GetUInt64("match_id"); set => Accessor.SetUInt64("match_id", value); }


  public string Text
  { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }

}
