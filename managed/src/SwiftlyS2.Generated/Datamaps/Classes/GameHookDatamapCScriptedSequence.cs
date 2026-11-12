using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCScriptedSequence(GameHooksService owner) : IGameHookDatamapCScriptedSequence
{
    internal readonly CScriptedSequenceScriptThinkHook CScriptedSequenceScriptThinkHook = new(owner);

    public ICScriptedSequenceScriptThinkHook ScriptThink => CScriptedSequenceScriptThinkHook;

    internal void UnregisterListeners()
    {
        CScriptedSequenceScriptThinkHook.UnregisterListeners();
    }
}