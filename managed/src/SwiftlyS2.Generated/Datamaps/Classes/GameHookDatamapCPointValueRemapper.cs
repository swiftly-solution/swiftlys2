using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPointValueRemapper : IGameHookDatamapCPointValueRemapper
{
    internal readonly CPointValueRemapperUpdateThinkHook CPointValueRemapperUpdateThinkHook = new();

    public ICPointValueRemapperUpdateThinkHook UpdateThink => CPointValueRemapperUpdateThinkHook;

    internal void UnregisterListeners()
    {
        CPointValueRemapperUpdateThinkHook.UnregisterListeners();
    }
}