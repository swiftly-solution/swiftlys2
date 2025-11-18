
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CWorkshop_GetContributors_Response : ITypedProtobuf<CWorkshop_GetContributors_Response>
{
  static CWorkshop_GetContributors_Response ITypedProtobuf<CWorkshop_GetContributors_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CWorkshop_GetContributors_ResponseImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldValueType<ulong> Contributors { get; }

}
