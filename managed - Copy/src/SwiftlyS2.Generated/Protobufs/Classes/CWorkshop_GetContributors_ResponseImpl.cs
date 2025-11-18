
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CWorkshop_GetContributors_ResponseImpl : TypedProtobuf<CWorkshop_GetContributors_Response>, CWorkshop_GetContributors_Response
{
  public CWorkshop_GetContributors_ResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldValueType<ulong> Contributors
  { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "contributors"); }

}
