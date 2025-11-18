
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientPlayerDecalSign : ITypedProtobuf<CMsgGCCStrike15_v2_ClientPlayerDecalSign>
{
  static CMsgGCCStrike15_v2_ClientPlayerDecalSign ITypedProtobuf<CMsgGCCStrike15_v2_ClientPlayerDecalSign>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientPlayerDecalSignImpl(handle, isManuallyAllocated);


  public PlayerDecalDigitalSignature Data { get; }


  public ulong Itemid { get; set; }

}
