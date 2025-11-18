
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientPollState : ITypedProtobuf<CMsgGCCStrike15_v2_ClientPollState>
{
  static CMsgGCCStrike15_v2_ClientPollState ITypedProtobuf<CMsgGCCStrike15_v2_ClientPollState>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientPollStateImpl(handle, isManuallyAllocated);


  public uint Pollid { get; set; }


  public IProtobufRepeatedFieldValueType<string> Names { get; }


  public IProtobufRepeatedFieldValueType<int> Values { get; }

}
