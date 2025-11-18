
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_ClientDeepStats_DeepStatsMatchImpl : TypedProtobuf<CMsgGCCStrike15_ClientDeepStats_DeepStatsMatch>, CMsgGCCStrike15_ClientDeepStats_DeepStatsMatch
{
  public CMsgGCCStrike15_ClientDeepStats_DeepStatsMatchImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public DeepPlayerStatsEntry Player
  { get => new DeepPlayerStatsEntryImpl(NativeNetMessages.GetNestedMessage(Address, "player"), false); }


  public IProtobufRepeatedFieldSubMessageType<DeepPlayerMatchEvent> Events
  { get => new ProtobufRepeatedFieldSubMessageType<DeepPlayerMatchEvent>(Accessor, "events"); }

}
