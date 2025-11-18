
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCommunity_GetGamePersonalDataEntries_Response : ITypedProtobuf<CCommunity_GetGamePersonalDataEntries_Response>
{
  static CCommunity_GetGamePersonalDataEntries_Response ITypedProtobuf<CCommunity_GetGamePersonalDataEntries_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CCommunity_GetGamePersonalDataEntries_ResponseImpl(handle, isManuallyAllocated);


  public uint Gceresult { get; set; }


  public IProtobufRepeatedFieldValueType<string> Entries { get; }


  public string ContinueToken { get; set; }


  public string ContinueText { get; set; }

}
