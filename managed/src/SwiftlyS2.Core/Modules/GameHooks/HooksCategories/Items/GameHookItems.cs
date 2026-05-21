using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookItem : IGameHookItem
{
    internal readonly CanAcquireItemHook CanAcquireHook = new();

    public ICanAcquireItemHook CanAcquire => CanAcquireHook;
}
