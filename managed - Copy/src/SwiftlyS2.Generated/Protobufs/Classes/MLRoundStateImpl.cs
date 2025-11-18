
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class MLRoundStateImpl : TypedProtobuf<MLRoundState>, MLRoundState
{
  public MLRoundStateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Phase
  { get => Accessor.GetString("phase"); set => Accessor.SetString("phase", value); }


  public ETeam WinTeam
  { get => (ETeam)Accessor.GetInt32("win_team"); set => Accessor.SetInt32("win_team", (int)value); }


  public string BombState
  { get => Accessor.GetString("bomb_state"); set => Accessor.SetString("bomb_state", value); }

}
