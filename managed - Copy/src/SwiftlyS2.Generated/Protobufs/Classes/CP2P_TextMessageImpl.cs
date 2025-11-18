
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CP2P_TextMessageImpl : TypedProtobuf<CP2P_TextMessage>, CP2P_TextMessage
{
  public CP2P_TextMessageImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public byte[] Text
  { get => Accessor.GetBytes("text"); set => Accessor.SetBytes("text", value); }

}
