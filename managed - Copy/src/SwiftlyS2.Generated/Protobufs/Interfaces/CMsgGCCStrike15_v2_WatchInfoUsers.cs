
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_WatchInfoUsers : ITypedProtobuf<CMsgGCCStrike15_v2_WatchInfoUsers>
{
  static CMsgGCCStrike15_v2_WatchInfoUsers ITypedProtobuf<CMsgGCCStrike15_v2_WatchInfoUsers>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_WatchInfoUsersImpl(handle, isManuallyAllocated);


  public uint RequestId { get; set; }


  public IProtobufRepeatedFieldValueType<uint> AccountIds { get; }


  public IProtobufRepeatedFieldSubMessageType<WatchableMatchInfo> WatchableMatchInfos { get; }


  public uint ExtendedTimeout { get; set; }

}
