
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockRequest : ITypedProtobuf<CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockRequest>
{
  static CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockRequest ITypedProtobuf<CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockRequest>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockRequestImpl(handle, isManuallyAllocated);


  public ulong ParamS { get; set; }


  public ulong ParamA { get; set; }


  public ulong ParamD { get; set; }


  public ulong ParamM { get; set; }

}
