
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGameServers_AggregationQuery_Response_Group : ITypedProtobuf<CGameServers_AggregationQuery_Response_Group>
{
  static CGameServers_AggregationQuery_Response_Group ITypedProtobuf<CGameServers_AggregationQuery_Response_Group>.Wrap(nint handle, bool isManuallyAllocated) => new CGameServers_AggregationQuery_Response_GroupImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldValueType<string> GroupValues { get; }


  public uint ServersEmpty { get; set; }


  public uint ServersFull { get; set; }


  public uint ServersTotal { get; set; }


  public uint PlayersHumans { get; set; }


  public uint PlayersBots { get; set; }


  public uint PlayerCapacity { get; set; }

}
