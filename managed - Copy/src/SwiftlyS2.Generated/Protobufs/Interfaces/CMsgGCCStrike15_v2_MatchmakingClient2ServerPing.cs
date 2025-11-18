
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchmakingClient2ServerPing : ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingClient2ServerPing>
{
  static CMsgGCCStrike15_v2_MatchmakingClient2ServerPing ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingClient2ServerPing>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchmakingClient2ServerPingImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<GameServerPing> Gameserverpings { get; }


  public int OffsetIndex { get; set; }


  public int FinalBatch { get; set; }


  public IProtobufRepeatedFieldSubMessageType<DataCenterPing> DataCenterPings { get; }


  public uint MaxPing { get; set; }


  public uint TestToken { get; set; }


  public byte[] SearchKey { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_Note> Notes { get; }


  public string DebugMessage { get; set; }

}
