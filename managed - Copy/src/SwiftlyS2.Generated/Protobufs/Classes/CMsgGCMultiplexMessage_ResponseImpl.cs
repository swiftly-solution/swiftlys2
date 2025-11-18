
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCMultiplexMessage_ResponseImpl : TypedProtobuf<CMsgGCMultiplexMessage_Response>, CMsgGCMultiplexMessage_Response
{
  public CMsgGCMultiplexMessage_ResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Msgtype
  { get => Accessor.GetUInt32("msgtype"); set => Accessor.SetUInt32("msgtype", value); }

}
