using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookItem( GameHooksService owner ) : IGameHookItem
{
    internal readonly CanAcquireItemHook CanAcquireHook = new(owner);
    internal readonly GetItemInLoadoutHook GetItemInLoadoutHook = new(owner);

    public ICanAcquireItemHook CanAcquire => CanAcquireHook;
    public IGetItemInLoadoutHook GetItemInLoadout => GetItemInLoadoutHook;
}
