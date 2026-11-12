using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncShatterglass(GameHooksService owner) : IGameHookDatamapCFuncShatterglass
{
    internal readonly CFuncShatterglassGlassThinkHook CFuncShatterglassGlassThinkHook = new(owner);

    public ICFuncShatterglassGlassThinkHook GlassThink => CFuncShatterglassGlassThinkHook;

    internal void UnregisterListeners()
    {
        CFuncShatterglassGlassThinkHook.UnregisterListeners();
    }
}