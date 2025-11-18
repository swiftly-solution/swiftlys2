
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgStoreGetUserDataResponseImpl : TypedProtobuf<CMsgStoreGetUserDataResponse>, CMsgStoreGetUserDataResponse
{
  public CMsgStoreGetUserDataResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Result
  { get => Accessor.GetInt32("result"); set => Accessor.SetInt32("result", value); }


  public int CurrencyDeprecated
  { get => Accessor.GetInt32("currency_deprecated"); set => Accessor.SetInt32("currency_deprecated", value); }


  public string CountryDeprecated
  { get => Accessor.GetString("country_deprecated"); set => Accessor.SetString("country_deprecated", value); }


  public uint PriceSheetVersion
  { get => Accessor.GetUInt32("price_sheet_version"); set => Accessor.SetUInt32("price_sheet_version", value); }


  public byte[] PriceSheet
  { get => Accessor.GetBytes("price_sheet"); set => Accessor.SetBytes("price_sheet", value); }

}
