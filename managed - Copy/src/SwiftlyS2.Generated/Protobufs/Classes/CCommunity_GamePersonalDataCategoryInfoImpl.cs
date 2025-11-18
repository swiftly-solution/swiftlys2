
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCommunity_GamePersonalDataCategoryInfoImpl : TypedProtobuf<CCommunity_GamePersonalDataCategoryInfo>, CCommunity_GamePersonalDataCategoryInfo
{
  public CCommunity_GamePersonalDataCategoryInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Type
  { get => Accessor.GetString("type"); set => Accessor.SetString("type", value); }


  public string LocalizationToken
  { get => Accessor.GetString("localization_token"); set => Accessor.SetString("localization_token", value); }


  public string TemplateFile
  { get => Accessor.GetString("template_file"); set => Accessor.SetString("template_file", value); }

}
