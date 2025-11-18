
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientAccountBalance : ITypedProtobuf<CMsgGCCStrike15_v2_ClientAccountBalance>
{
  static CMsgGCCStrike15_v2_ClientAccountBalance ITypedProtobuf<CMsgGCCStrike15_v2_ClientAccountBalance>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientAccountBalanceImpl(handle, isManuallyAllocated);


  public ulong Amount { get; set; }


  public string Url { get; set; }

}
