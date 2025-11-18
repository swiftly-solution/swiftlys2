
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCommunity_GetGamePersonalDataCategories_ResponseImpl : TypedProtobuf<CCommunity_GetGamePersonalDataCategories_Response>, CCommunity_GetGamePersonalDataCategories_Response
{
  public CCommunity_GetGamePersonalDataCategories_ResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CCommunity_GamePersonalDataCategoryInfo> Categories
  { get => new ProtobufRepeatedFieldSubMessageType<CCommunity_GamePersonalDataCategoryInfo>(Accessor, "categories"); }


  public string AppAssetsBasename
  { get => Accessor.GetString("app_assets_basename"); set => Accessor.SetString("app_assets_basename", value); }

}
