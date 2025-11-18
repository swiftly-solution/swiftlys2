
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoFullPacket : ITypedProtobuf<CDemoFullPacket>
{
  static CDemoFullPacket ITypedProtobuf<CDemoFullPacket>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoFullPacketImpl(handle, isManuallyAllocated);


  public CDemoStringTables StringTable { get; }


  public CDemoPacket Packet { get; }

}
