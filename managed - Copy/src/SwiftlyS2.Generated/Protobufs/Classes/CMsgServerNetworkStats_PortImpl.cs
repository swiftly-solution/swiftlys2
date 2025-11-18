
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgServerNetworkStats_PortImpl : TypedProtobuf<CMsgServerNetworkStats_Port>, CMsgServerNetworkStats_Port
{
  public CMsgServerNetworkStats_PortImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Port
  { get => Accessor.GetInt32("port"); set => Accessor.SetInt32("port", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }

}
