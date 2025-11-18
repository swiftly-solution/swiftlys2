
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGameServers_AggregationQuery_RequestImpl : TypedProtobuf<CGameServers_AggregationQuery_Request>, CGameServers_AggregationQuery_Request
{
  public CGameServers_AggregationQuery_RequestImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Filter
  { get => Accessor.GetString("filter"); set => Accessor.SetString("filter", value); }


  public IProtobufRepeatedFieldValueType<string> GroupFields
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "group_fields"); }

}
