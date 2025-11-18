using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Core.GameEvents;

internal class GameEvent<T> where T : IGameEvent<T> {

  private GameEventAccessor _Accessor { get; init; }

  public IGameEventAccessor Accessor => _Accessor;

  public GameEvent(nint address) {
    _Accessor = new GameEventAccessor(address);
  }

  public void Dispose() {
    _Accessor.Dispose();
  }

  public bool DontBroadcast { 
    get => Accessor.DontBroadcast;
    set => Accessor.DontBroadcast = value;
  }

}