using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookItems : IGameHookItems
{
    internal readonly CanAcquireItemEvents CanAcquireEvents = new();

    public ICanAcquireItemEvents CanAcquire => CanAcquireEvents;
}
