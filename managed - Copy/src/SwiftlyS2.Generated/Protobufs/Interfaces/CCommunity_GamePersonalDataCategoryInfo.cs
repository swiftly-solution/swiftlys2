
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCommunity_GamePersonalDataCategoryInfo : ITypedProtobuf<CCommunity_GamePersonalDataCategoryInfo>
{
  static CCommunity_GamePersonalDataCategoryInfo ITypedProtobuf<CCommunity_GamePersonalDataCategoryInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CCommunity_GamePersonalDataCategoryInfoImpl(handle, isManuallyAllocated);


  public string Type { get; set; }


  public string LocalizationToken { get; set; }


  public string TemplateFile { get; set; }

}
