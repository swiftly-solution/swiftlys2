
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCommunity_GetGamePersonalDataCategories_Response : ITypedProtobuf<CCommunity_GetGamePersonalDataCategories_Response>
{
  static CCommunity_GetGamePersonalDataCategories_Response ITypedProtobuf<CCommunity_GetGamePersonalDataCategories_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CCommunity_GetGamePersonalDataCategories_ResponseImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CCommunity_GamePersonalDataCategoryInfo> Categories { get; }


  public string AppAssetsBasename { get; set; }

}
