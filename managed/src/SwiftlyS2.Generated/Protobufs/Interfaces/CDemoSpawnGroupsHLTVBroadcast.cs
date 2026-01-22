
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoSpawnGroupsHLTVBroadcast : ITypedProtobuf<CDemoSpawnGroupsHLTVBroadcast>
{
  static CDemoSpawnGroupsHLTVBroadcast ITypedProtobuf<CDemoSpawnGroupsHLTVBroadcast>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoSpawnGroupsHLTVBroadcastImpl(handle, isManuallyAllocated);


  public byte[] Data { get; set; }

}
