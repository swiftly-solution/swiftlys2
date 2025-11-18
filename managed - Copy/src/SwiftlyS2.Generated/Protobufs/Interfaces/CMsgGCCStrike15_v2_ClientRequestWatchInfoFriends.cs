
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientRequestWatchInfoFriends : ITypedProtobuf<CMsgGCCStrike15_v2_ClientRequestWatchInfoFriends>
{
  static CMsgGCCStrike15_v2_ClientRequestWatchInfoFriends ITypedProtobuf<CMsgGCCStrike15_v2_ClientRequestWatchInfoFriends>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientRequestWatchInfoFriendsImpl(handle, isManuallyAllocated);


  public uint RequestId { get; set; }


  public IProtobufRepeatedFieldValueType<uint> AccountIds { get; }


  public ulong Serverid { get; set; }


  public ulong Matchid { get; set; }


  public uint ClientLauncher { get; set; }


  public IProtobufRepeatedFieldSubMessageType<DataCenterPing> DataCenterPings { get; }

}
