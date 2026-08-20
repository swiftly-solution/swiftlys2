using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncShatterglass : IGameHookDatamapCFuncShatterglass
{
    internal readonly CFuncShatterglassGlassThinkHook CFuncShatterglassGlassThinkHook = new();

    public ICFuncShatterglassGlassThinkHook GlassThink => CFuncShatterglassGlassThinkHook;

    internal void UnregisterListeners()
    {
        CFuncShatterglassGlassThinkHook.UnregisterListeners();
    }
}