
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class NetMessagePacketStartImpl : TypedProtobuf<NetMessagePacketStart>, NetMessagePacketStart
{
  public NetMessagePacketStartImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


}
