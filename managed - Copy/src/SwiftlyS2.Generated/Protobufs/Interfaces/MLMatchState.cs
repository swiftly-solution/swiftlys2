
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface MLMatchState : ITypedProtobuf<MLMatchState>
{
  static MLMatchState ITypedProtobuf<MLMatchState>.Wrap(nint handle, bool isManuallyAllocated) => new MLMatchStateImpl(handle, isManuallyAllocated);


  public string GameMode { get; set; }


  public string Phase { get; set; }


  public int Round { get; set; }


  public int ScoreCt { get; set; }


  public int ScoreT { get; set; }

}
