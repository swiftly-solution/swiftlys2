
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface MLGameState : ITypedProtobuf<MLGameState>
{
  static MLGameState ITypedProtobuf<MLGameState>.Wrap(nint handle, bool isManuallyAllocated) => new MLGameStateImpl(handle, isManuallyAllocated);


  public MLMatchState Match { get; }


  public MLRoundState Round { get; }


  public IProtobufRepeatedFieldSubMessageType<MLPlayerState> Players { get; }

}
