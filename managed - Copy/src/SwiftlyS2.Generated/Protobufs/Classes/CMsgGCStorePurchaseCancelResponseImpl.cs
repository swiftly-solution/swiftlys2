
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCStorePurchaseCancelResponseImpl : TypedProtobuf<CMsgGCStorePurchaseCancelResponse>, CMsgGCStorePurchaseCancelResponse
{
  public CMsgGCStorePurchaseCancelResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Result
  { get => Accessor.GetUInt32("result"); set => Accessor.SetUInt32("result", value); }

}
