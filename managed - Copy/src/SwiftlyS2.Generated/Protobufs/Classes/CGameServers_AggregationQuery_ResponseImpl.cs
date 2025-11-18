
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGameServers_AggregationQuery_ResponseImpl : TypedProtobuf<CGameServers_AggregationQuery_Response>, CGameServers_AggregationQuery_Response
{
  public CGameServers_AggregationQuery_ResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CGameServers_AggregationQuery_Response_Group> Groups
  { get => new ProtobufRepeatedFieldSubMessageType<CGameServers_AggregationQuery_Response_Group>(Accessor, "groups"); }

}
