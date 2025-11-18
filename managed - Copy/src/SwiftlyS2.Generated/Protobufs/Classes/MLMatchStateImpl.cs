
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class MLMatchStateImpl : TypedProtobuf<MLMatchState>, MLMatchState
{
  public MLMatchStateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string GameMode
  { get => Accessor.GetString("game_mode"); set => Accessor.SetString("game_mode", value); }


  public string Phase
  { get => Accessor.GetString("phase"); set => Accessor.SetString("phase", value); }


  public int Round
  { get => Accessor.GetInt32("round"); set => Accessor.SetInt32("round", value); }


  public int ScoreCt
  { get => Accessor.GetInt32("score_ct"); set => Accessor.SetInt32("score_ct", value); }


  public int ScoreT
  { get => Accessor.GetInt32("score_t"); set => Accessor.SetInt32("score_t", value); }

}
