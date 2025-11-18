
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientRequestJoinFriendData : ITypedProtobuf<CMsgGCCStrike15_v2_ClientRequestJoinFriendData>
{
  static CMsgGCCStrike15_v2_ClientRequestJoinFriendData ITypedProtobuf<CMsgGCCStrike15_v2_ClientRequestJoinFriendData>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientRequestJoinFriendDataImpl(handle, isManuallyAllocated);


  public uint Version { get; set; }


  public uint AccountId { get; set; }


  public uint JoinToken { get; set; }


  public uint JoinIpp { get; set; }


  public CMsgGCCStrike15_v2_MatchmakingGC2ClientReserve Res { get; }


  public string Errormsg { get; set; }

}
