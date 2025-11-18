
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class OperationalStatisticsPacketImpl : TypedProtobuf<OperationalStatisticsPacket>, OperationalStatisticsPacket
{
  public OperationalStatisticsPacketImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Packetid
  { get => Accessor.GetInt32("packetid"); set => Accessor.SetInt32("packetid", value); }


  public int Mstimestamp
  { get => Accessor.GetInt32("mstimestamp"); set => Accessor.SetInt32("mstimestamp", value); }


  public IProtobufRepeatedFieldSubMessageType<OperationalStatisticElement> Values
  { get => new ProtobufRepeatedFieldSubMessageType<OperationalStatisticElement>(Accessor, "values"); }

}
