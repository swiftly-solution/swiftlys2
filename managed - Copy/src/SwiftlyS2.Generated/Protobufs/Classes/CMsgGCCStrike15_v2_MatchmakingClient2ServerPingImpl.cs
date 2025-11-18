
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchmakingClient2ServerPingImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchmakingClient2ServerPing>, CMsgGCCStrike15_v2_MatchmakingClient2ServerPing
{
  public CMsgGCCStrike15_v2_MatchmakingClient2ServerPingImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<GameServerPing> Gameserverpings
  { get => new ProtobufRepeatedFieldSubMessageType<GameServerPing>(Accessor, "gameserverpings"); }


  public int OffsetIndex
  { get => Accessor.GetInt32("offset_index"); set => Accessor.SetInt32("offset_index", value); }


  public int FinalBatch
  { get => Accessor.GetInt32("final_batch"); set => Accessor.SetInt32("final_batch", value); }


  public IProtobufRepeatedFieldSubMessageType<DataCenterPing> DataCenterPings
  { get => new ProtobufRepeatedFieldSubMessageType<DataCenterPing>(Accessor, "data_center_pings"); }


  public uint MaxPing
  { get => Accessor.GetUInt32("max_ping"); set => Accessor.SetUInt32("max_ping", value); }


  public uint TestToken
  { get => Accessor.GetUInt32("test_token"); set => Accessor.SetUInt32("test_token", value); }


  public byte[] SearchKey
  { get => Accessor.GetBytes("search_key"); set => Accessor.SetBytes("search_key", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_Note> Notes
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_Note>(Accessor, "notes"); }


  public string DebugMessage
  { get => Accessor.GetString("debug_message"); set => Accessor.SetString("debug_message", value); }

}
