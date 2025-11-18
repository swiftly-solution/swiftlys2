
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class C2S_CONNECTION_MessageImpl : TypedProtobuf<C2S_CONNECTION_Message>, C2S_CONNECTION_Message
{
  public C2S_CONNECTION_MessageImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string AddonName
  { get => Accessor.GetString("addon_name"); set => Accessor.SetString("addon_name", value); }


  public C2S_CONNECT_SameProcessCheck LocalhostSameProcessCheck
  { get => new C2S_CONNECT_SameProcessCheckImpl(NativeNetMessages.GetNestedMessage(Address, "localhost_same_process_check"), false); }

}
