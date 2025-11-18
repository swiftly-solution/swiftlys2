
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CWorkshop_GetContributors_Request : ITypedProtobuf<CWorkshop_GetContributors_Request>
{
  static CWorkshop_GetContributors_Request ITypedProtobuf<CWorkshop_GetContributors_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CWorkshop_GetContributors_RequestImpl(handle, isManuallyAllocated);


  public uint Appid { get; set; }


  public uint Gameitemid { get; set; }

}
