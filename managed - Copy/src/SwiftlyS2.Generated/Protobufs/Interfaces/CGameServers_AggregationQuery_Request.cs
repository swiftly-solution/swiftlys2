
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGameServers_AggregationQuery_Request : ITypedProtobuf<CGameServers_AggregationQuery_Request>
{
  static CGameServers_AggregationQuery_Request ITypedProtobuf<CGameServers_AggregationQuery_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CGameServers_AggregationQuery_RequestImpl(handle, isManuallyAllocated);


  public string Filter { get; set; }


  public IProtobufRepeatedFieldValueType<string> GroupFields { get; }

}
