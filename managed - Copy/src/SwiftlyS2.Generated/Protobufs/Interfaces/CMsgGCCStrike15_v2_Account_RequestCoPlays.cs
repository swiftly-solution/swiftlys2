
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_Account_RequestCoPlays : ITypedProtobuf<CMsgGCCStrike15_v2_Account_RequestCoPlays>
{
  static CMsgGCCStrike15_v2_Account_RequestCoPlays ITypedProtobuf<CMsgGCCStrike15_v2_Account_RequestCoPlays>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_Account_RequestCoPlaysImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_Account_RequestCoPlays_Player> Players { get; }


  public uint Servertime { get; set; }

}
