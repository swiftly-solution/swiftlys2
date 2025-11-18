
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCToGCIsTrustedServer : ITypedProtobuf<CMsgGCToGCIsTrustedServer>
{
  static CMsgGCToGCIsTrustedServer ITypedProtobuf<CMsgGCToGCIsTrustedServer>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCToGCIsTrustedServerImpl(handle, isManuallyAllocated);


  public ulong SteamId { get; set; }

}
