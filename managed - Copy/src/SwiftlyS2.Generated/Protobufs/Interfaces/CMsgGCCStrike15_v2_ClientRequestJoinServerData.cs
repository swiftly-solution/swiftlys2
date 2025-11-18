
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientRequestJoinServerData : ITypedProtobuf<CMsgGCCStrike15_v2_ClientRequestJoinServerData>
{
  static CMsgGCCStrike15_v2_ClientRequestJoinServerData ITypedProtobuf<CMsgGCCStrike15_v2_ClientRequestJoinServerData>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientRequestJoinServerDataImpl(handle, isManuallyAllocated);


  public uint Version { get; set; }


  public uint AccountId { get; set; }


  public ulong Serverid { get; set; }


  public uint ServerIp { get; set; }


  public uint ServerPort { get; set; }


  public CMsgGCCStrike15_v2_MatchmakingGC2ClientReserve Res { get; }


  public string Errormsg { get; set; }

}
