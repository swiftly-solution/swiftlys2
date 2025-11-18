
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCStorePurchaseFinalizeResponseImpl : TypedProtobuf<CMsgGCStorePurchaseFinalizeResponse>, CMsgGCStorePurchaseFinalizeResponse
{
  public CMsgGCStorePurchaseFinalizeResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Result
  { get => Accessor.GetUInt32("result"); set => Accessor.SetUInt32("result", value); }


  public IProtobufRepeatedFieldValueType<ulong> ItemIds
  { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "item_ids"); }

}
