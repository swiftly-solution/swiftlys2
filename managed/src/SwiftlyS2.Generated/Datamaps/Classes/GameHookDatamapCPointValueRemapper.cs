using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPointValueRemapper(GameHooksService owner) : IGameHookDatamapCPointValueRemapper
{
    internal readonly CPointValueRemapperUpdateThinkHook CPointValueRemapperUpdateThinkHook = new(owner);

    public ICPointValueRemapperUpdateThinkHook UpdateThink => CPointValueRemapperUpdateThinkHook;

    internal void UnregisterListeners()
    {
        CPointValueRemapperUpdateThinkHook.UnregisterListeners();
    }
}