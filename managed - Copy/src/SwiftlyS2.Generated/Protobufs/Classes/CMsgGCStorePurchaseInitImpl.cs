
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCStorePurchaseInitImpl : TypedProtobuf<CMsgGCStorePurchaseInit>, CMsgGCStorePurchaseInit
{
  public CMsgGCStorePurchaseInitImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Country
  { get => Accessor.GetString("country"); set => Accessor.SetString("country", value); }


  public int Language
  { get => Accessor.GetInt32("language"); set => Accessor.SetInt32("language", value); }


  public int Currency
  { get => Accessor.GetInt32("currency"); set => Accessor.SetInt32("currency", value); }


  public IProtobufRepeatedFieldSubMessageType<CGCStorePurchaseInit_LineItem> LineItems
  { get => new ProtobufRepeatedFieldSubMessageType<CGCStorePurchaseInit_LineItem>(Accessor, "line_items"); }

}
