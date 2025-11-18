
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface MLRoundState : ITypedProtobuf<MLRoundState>
{
  static MLRoundState ITypedProtobuf<MLRoundState>.Wrap(nint handle, bool isManuallyAllocated) => new MLRoundStateImpl(handle, isManuallyAllocated);


  public string Phase { get; set; }


  public ETeam WinTeam { get; set; }


  public string BombState { get; set; }

}
