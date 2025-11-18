
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCUpdateSessionIP : ITypedProtobuf<CMsgGCUpdateSessionIP>
{
  static CMsgGCUpdateSessionIP ITypedProtobuf<CMsgGCUpdateSessionIP>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCUpdateSessionIPImpl(handle, isManuallyAllocated);


  public ulong Steamid { get; set; }


  public uint Ip { get; set; }

}
