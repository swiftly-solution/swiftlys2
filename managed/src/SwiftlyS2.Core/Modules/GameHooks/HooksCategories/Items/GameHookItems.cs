using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookItem : IGameHookItem
{
    internal readonly CanAcquireItemHook CanAcquireHook = new();
    internal readonly GetItemInLoadoutHook GetItemInLoadoutHook = new();

    public ICanAcquireItemHook CanAcquire => CanAcquireHook;
    public IGetItemInLoadoutHook GetItemInLoadout => GetItemInLoadoutHook;
}
