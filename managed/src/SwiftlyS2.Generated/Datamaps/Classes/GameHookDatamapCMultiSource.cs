using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCMultiSource(GameHooksService owner) : IGameHookDatamapCMultiSource
{
    internal readonly CMultiSourceRegisterHook CMultiSourceRegisterHook = new(owner);

    public ICMultiSourceRegisterHook Register => CMultiSourceRegisterHook;

    internal void UnregisterListeners()
    {
        CMultiSourceRegisterHook.UnregisterListeners();
    }
}