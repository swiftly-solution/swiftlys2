
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockResponseImpl : TypedProtobuf<CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockResponse>, CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockResponse
{
  public CMsgGCCStrike15_v2_Client2GCEconPreviewDataBlockResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public CEconItemPreviewDataBlock Iteminfo
  { get => new CEconItemPreviewDataBlockImpl(NativeNetMessages.GetNestedMessage(Address, "iteminfo"), false); }

}
