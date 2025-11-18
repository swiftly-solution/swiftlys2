
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_Fantasy : ITypedProtobuf<CMsgGCCStrike15_v2_Fantasy>
{
  static CMsgGCCStrike15_v2_Fantasy ITypedProtobuf<CMsgGCCStrike15_v2_Fantasy>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_FantasyImpl(handle, isManuallyAllocated);


  public uint EventId { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_Fantasy_FantasyTeam> Teams { get; }

}
