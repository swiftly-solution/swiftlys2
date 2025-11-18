
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface DeepPlayerMatchEvent : ITypedProtobuf<DeepPlayerMatchEvent>
{
  static DeepPlayerMatchEvent ITypedProtobuf<DeepPlayerMatchEvent>.Wrap(nint handle, bool isManuallyAllocated) => new DeepPlayerMatchEventImpl(handle, isManuallyAllocated);


  public uint Accountid { get; set; }


  public ulong MatchId { get; set; }


  public uint EventId { get; set; }


  public uint EventType { get; set; }


  public bool BPlayingCt { get; set; }


  public int UserPosX { get; set; }


  public int UserPosY { get; set; }


  public int UserPosZ { get; set; }


  public uint UserDefidx { get; set; }


  public int OtherPosX { get; set; }


  public int OtherPosY { get; set; }


  public int OtherPosZ { get; set; }


  public uint OtherDefidx { get; set; }


  public int EventData { get; set; }

}
