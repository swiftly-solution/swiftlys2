
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface OperationalStatisticsPacket : ITypedProtobuf<OperationalStatisticsPacket>
{
  static OperationalStatisticsPacket ITypedProtobuf<OperationalStatisticsPacket>.Wrap(nint handle, bool isManuallyAllocated) => new OperationalStatisticsPacketImpl(handle, isManuallyAllocated);


  public int Packetid { get; set; }


  public int Mstimestamp { get; set; }


  public IProtobufRepeatedFieldSubMessageType<OperationalStatisticElement> Values { get; }

}
