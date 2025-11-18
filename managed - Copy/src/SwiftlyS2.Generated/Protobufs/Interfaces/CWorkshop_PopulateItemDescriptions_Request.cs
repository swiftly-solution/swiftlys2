
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CWorkshop_PopulateItemDescriptions_Request : ITypedProtobuf<CWorkshop_PopulateItemDescriptions_Request>
{
  static CWorkshop_PopulateItemDescriptions_Request ITypedProtobuf<CWorkshop_PopulateItemDescriptions_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CWorkshop_PopulateItemDescriptions_RequestImpl(handle, isManuallyAllocated);


  public uint Appid { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CWorkshop_PopulateItemDescriptions_Request_ItemDescriptionsLanguageBlock> Languages { get; }

}
