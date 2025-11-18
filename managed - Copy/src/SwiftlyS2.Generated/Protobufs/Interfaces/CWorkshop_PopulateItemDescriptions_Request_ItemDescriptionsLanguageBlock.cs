
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CWorkshop_PopulateItemDescriptions_Request_ItemDescriptionsLanguageBlock : ITypedProtobuf<CWorkshop_PopulateItemDescriptions_Request_ItemDescriptionsLanguageBlock>
{
  static CWorkshop_PopulateItemDescriptions_Request_ItemDescriptionsLanguageBlock ITypedProtobuf<CWorkshop_PopulateItemDescriptions_Request_ItemDescriptionsLanguageBlock>.Wrap(nint handle, bool isManuallyAllocated) => new CWorkshop_PopulateItemDescriptions_Request_ItemDescriptionsLanguageBlockImpl(handle, isManuallyAllocated);


  public string Language { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CWorkshop_PopulateItemDescriptions_Request_SingleItemDescription> Descriptions { get; }

}
