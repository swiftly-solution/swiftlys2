
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_HltvReplay : ITypedProtobuf<CSVCMsg_HltvReplay>
{
  static CSVCMsg_HltvReplay ITypedProtobuf<CSVCMsg_HltvReplay>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_HltvReplayImpl(handle, isManuallyAllocated);


  public int Delay { get; set; }


  public int PrimaryTarget { get; set; }


  public int ReplayStopAt { get; set; }


  public int ReplayStartAt { get; set; }


  public int ReplaySlowdownBegin { get; set; }


  public int ReplaySlowdownEnd { get; set; }


  public float ReplaySlowdownRate { get; set; }


  public int Reason { get; set; }

}
