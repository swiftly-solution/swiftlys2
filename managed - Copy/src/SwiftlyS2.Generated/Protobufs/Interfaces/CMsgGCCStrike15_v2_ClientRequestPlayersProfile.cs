
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientRequestPlayersProfile : ITypedProtobuf<CMsgGCCStrike15_v2_ClientRequestPlayersProfile>
{
  static CMsgGCCStrike15_v2_ClientRequestPlayersProfile ITypedProtobuf<CMsgGCCStrike15_v2_ClientRequestPlayersProfile>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientRequestPlayersProfileImpl(handle, isManuallyAllocated);


  public uint RequestIdDeprecated { get; set; }


  public IProtobufRepeatedFieldValueType<uint> AccountIdsDeprecated { get; }


  public uint AccountId { get; set; }


  public uint RequestLevel { get; set; }

}
