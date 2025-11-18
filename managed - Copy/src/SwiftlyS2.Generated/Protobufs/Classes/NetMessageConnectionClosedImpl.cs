
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class NetMessageConnectionClosedImpl : TypedProtobuf<NetMessageConnectionClosed>, NetMessageConnectionClosed
{
  public NetMessageConnectionClosedImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Reason
  { get => Accessor.GetUInt32("reason"); set => Accessor.SetUInt32("reason", value); }


  public string Message
  { get => Accessor.GetString("message"); set => Accessor.SetString("message", value); }

}
