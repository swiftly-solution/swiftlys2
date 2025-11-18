
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface WatchableMatchInfo : ITypedProtobuf<WatchableMatchInfo>
{
  static WatchableMatchInfo ITypedProtobuf<WatchableMatchInfo>.Wrap(nint handle, bool isManuallyAllocated) => new WatchableMatchInfoImpl(handle, isManuallyAllocated);


  public uint ServerIp { get; set; }


  public uint TvPort { get; set; }


  public uint TvSpectators { get; set; }


  public uint TvTime { get; set; }


  public byte[] TvWatchPassword { get; set; }


  public ulong ClDecryptdataKey { get; set; }


  public ulong ClDecryptdataKeyPub { get; set; }


  public uint GameType { get; set; }


  public string GameMapgroup { get; set; }


  public string GameMap { get; set; }


  public ulong ServerId { get; set; }


  public ulong MatchId { get; set; }


  public ulong ReservationId { get; set; }

}
