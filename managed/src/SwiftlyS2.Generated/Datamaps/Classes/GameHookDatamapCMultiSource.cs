using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCMultiSource : IGameHookDatamapCMultiSource
{
    internal readonly CMultiSourceRegisterHook CMultiSourceRegisterHook = new();

    public ICMultiSourceRegisterHook Register => CMultiSourceRegisterHook;

    internal void UnregisterListeners()
    {
        CMultiSourceRegisterHook.UnregisterListeners();
    }
}