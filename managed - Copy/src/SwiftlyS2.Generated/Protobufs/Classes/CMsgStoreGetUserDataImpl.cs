
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgStoreGetUserDataImpl : TypedProtobuf<CMsgStoreGetUserData>, CMsgStoreGetUserData
{
  public CMsgStoreGetUserDataImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint PriceSheetVersion
  { get => Accessor.GetUInt32("price_sheet_version"); set => Accessor.SetUInt32("price_sheet_version", value); }


  public int Currency
  { get => Accessor.GetInt32("currency"); set => Accessor.SetInt32("currency", value); }

}
