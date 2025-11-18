
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface NetMessagePacketStart : ITypedProtobuf<NetMessagePacketStart>
{
  static NetMessagePacketStart ITypedProtobuf<NetMessagePacketStart>.Wrap(nint handle, bool isManuallyAllocated) => new NetMessagePacketStartImpl(handle, isManuallyAllocated);


}
