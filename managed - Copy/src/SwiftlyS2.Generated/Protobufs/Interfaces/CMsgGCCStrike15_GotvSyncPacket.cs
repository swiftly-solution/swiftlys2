
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_GotvSyncPacket : ITypedProtobuf<CMsgGCCStrike15_GotvSyncPacket>
{
  static CMsgGCCStrike15_GotvSyncPacket ITypedProtobuf<CMsgGCCStrike15_GotvSyncPacket>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_GotvSyncPacketImpl(handle, isManuallyAllocated);


  public CEngineGotvSyncPacket Data { get; }

}
