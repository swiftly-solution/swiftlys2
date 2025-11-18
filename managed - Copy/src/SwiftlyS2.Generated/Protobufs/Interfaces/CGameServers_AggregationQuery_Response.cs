
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGameServers_AggregationQuery_Response : ITypedProtobuf<CGameServers_AggregationQuery_Response>
{
  static CGameServers_AggregationQuery_Response ITypedProtobuf<CGameServers_AggregationQuery_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CGameServers_AggregationQuery_ResponseImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CGameServers_AggregationQuery_Response_Group> Groups { get; }

}
