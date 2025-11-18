
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CWorkshop_GetContributors_RequestImpl : TypedProtobuf<CWorkshop_GetContributors_Request>, CWorkshop_GetContributors_Request
{
  public CWorkshop_GetContributors_RequestImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Appid
  { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }


  public uint Gameitemid
  { get => Accessor.GetUInt32("gameitemid"); set => Accessor.SetUInt32("gameitemid", value); }

}
