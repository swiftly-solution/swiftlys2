
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class C2S_CONNECT_SameProcessCheckImpl : TypedProtobuf<C2S_CONNECT_SameProcessCheck>, C2S_CONNECT_SameProcessCheck
{
  public C2S_CONNECT_SameProcessCheckImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong LocalhostProcessId
  { get => Accessor.GetUInt64("localhost_process_id"); set => Accessor.SetUInt64("localhost_process_id", value); }


  public ulong Key
  { get => Accessor.GetUInt64("key"); set => Accessor.SetUInt64("key", value); }

}
