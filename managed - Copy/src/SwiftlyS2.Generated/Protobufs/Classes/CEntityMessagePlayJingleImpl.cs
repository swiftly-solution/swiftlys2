
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CEntityMessagePlayJingleImpl : TypedProtobuf<CEntityMessagePlayJingle>, CEntityMessagePlayJingle
{
  public CEntityMessagePlayJingleImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public CEntityMsg EntityMsg
  { get => new CEntityMsgImpl(NativeNetMessages.GetNestedMessage(Address, "entity_msg"), false); }

}
