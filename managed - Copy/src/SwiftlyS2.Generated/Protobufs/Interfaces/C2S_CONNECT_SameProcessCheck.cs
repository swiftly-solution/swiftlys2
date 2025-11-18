
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface C2S_CONNECT_SameProcessCheck : ITypedProtobuf<C2S_CONNECT_SameProcessCheck>
{
  static C2S_CONNECT_SameProcessCheck ITypedProtobuf<C2S_CONNECT_SameProcessCheck>.Wrap(nint handle, bool isManuallyAllocated) => new C2S_CONNECT_SameProcessCheckImpl(handle, isManuallyAllocated);


  public ulong LocalhostProcessId { get; set; }


  public ulong Key { get; set; }

}
