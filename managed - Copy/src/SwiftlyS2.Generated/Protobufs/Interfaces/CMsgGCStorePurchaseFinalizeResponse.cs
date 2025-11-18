
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCStorePurchaseFinalizeResponse : ITypedProtobuf<CMsgGCStorePurchaseFinalizeResponse>
{
  static CMsgGCStorePurchaseFinalizeResponse ITypedProtobuf<CMsgGCStorePurchaseFinalizeResponse>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCStorePurchaseFinalizeResponseImpl(handle, isManuallyAllocated);


  public uint Result { get; set; }


  public IProtobufRepeatedFieldValueType<ulong> ItemIds { get; }

}
