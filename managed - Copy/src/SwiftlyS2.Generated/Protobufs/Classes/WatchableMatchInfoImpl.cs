
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class WatchableMatchInfoImpl : TypedProtobuf<WatchableMatchInfo>, WatchableMatchInfo
{
  public WatchableMatchInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint ServerIp
  { get => Accessor.GetUInt32("server_ip"); set => Accessor.SetUInt32("server_ip", value); }


  public uint TvPort
  { get => Accessor.GetUInt32("tv_port"); set => Accessor.SetUInt32("tv_port", value); }


  public uint TvSpectators
  { get => Accessor.GetUInt32("tv_spectators"); set => Accessor.SetUInt32("tv_spectators", value); }


  public uint TvTime
  { get => Accessor.GetUInt32("tv_time"); set => Accessor.SetUInt32("tv_time", value); }


  public byte[] TvWatchPassword
  { get => Accessor.GetBytes("tv_watch_password"); set => Accessor.SetBytes("tv_watch_password", value); }


  public ulong ClDecryptdataKey
  { get => Accessor.GetUInt64("cl_decryptdata_key"); set => Accessor.SetUInt64("cl_decryptdata_key", value); }


  public ulong ClDecryptdataKeyPub
  { get => Accessor.GetUInt64("cl_decryptdata_key_pub"); set => Accessor.SetUInt64("cl_decryptdata_key_pub", value); }


  public uint GameType
  { get => Accessor.GetUInt32("game_type"); set => Accessor.SetUInt32("game_type", value); }


  public string GameMapgroup
  { get => Accessor.GetString("game_mapgroup"); set => Accessor.SetString("game_mapgroup", value); }


  public string GameMap
  { get => Accessor.GetString("game_map"); set => Accessor.SetString("game_map", value); }


  public ulong ServerId
  { get => Accessor.GetUInt64("server_id"); set => Accessor.SetUInt64("server_id", value); }


  public ulong MatchId
  { get => Accessor.GetUInt64("match_id"); set => Accessor.SetUInt64("match_id", value); }


  public ulong ReservationId
  { get => Accessor.GetUInt64("reservation_id"); set => Accessor.SetUInt64("reservation_id", value); }

}
