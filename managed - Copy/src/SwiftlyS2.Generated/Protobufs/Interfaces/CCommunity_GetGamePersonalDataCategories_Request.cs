
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCommunity_GetGamePersonalDataCategories_Request : ITypedProtobuf<CCommunity_GetGamePersonalDataCategories_Request>
{
  static CCommunity_GetGamePersonalDataCategories_Request ITypedProtobuf<CCommunity_GetGamePersonalDataCategories_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CCommunity_GetGamePersonalDataCategories_RequestImpl(handle, isManuallyAllocated);


  public uint Appid { get; set; }

}
