
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCStorePurchaseInitResponseImpl : TypedProtobuf<CMsgGCStorePurchaseInitResponse>, CMsgGCStorePurchaseInitResponse
{
  public CMsgGCStorePurchaseInitResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Result
  { get => Accessor.GetInt32("result"); set => Accessor.SetInt32("result", value); }


  public ulong TxnId
  { get => Accessor.GetUInt64("txn_id"); set => Accessor.SetUInt64("txn_id", value); }


  public string Url
  { get => Accessor.GetString("url"); set => Accessor.SetString("url", value); }


  public IProtobufRepeatedFieldValueType<ulong> ItemIds
  { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "item_ids"); }

}
