
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class DataCenterPingImpl : TypedProtobuf<DataCenterPing>, DataCenterPing
{
  public DataCenterPingImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint DataCenterId
  { get => Accessor.GetUInt32("data_center_id"); set => Accessor.SetUInt32("data_center_id", value); }


  public int Ping
  { get => Accessor.GetInt32("ping"); set => Accessor.SetInt32("ping", value); }

}
