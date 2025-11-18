
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface DataCenterPing : ITypedProtobuf<DataCenterPing>
{
  static DataCenterPing ITypedProtobuf<DataCenterPing>.Wrap(nint handle, bool isManuallyAllocated) => new DataCenterPingImpl(handle, isManuallyAllocated);


  public uint DataCenterId { get; set; }


  public int Ping { get; set; }

}
