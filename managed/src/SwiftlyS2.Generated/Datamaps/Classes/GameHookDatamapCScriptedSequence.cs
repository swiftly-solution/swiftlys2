using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCScriptedSequence : IGameHookDatamapCScriptedSequence
{
    internal readonly CScriptedSequenceScriptThinkHook CScriptedSequenceScriptThinkHook = new();

    public ICScriptedSequenceScriptThinkHook ScriptThink => CScriptedSequenceScriptThinkHook;

    internal void UnregisterListeners()
    {
        CScriptedSequenceScriptThinkHook.UnregisterListeners();
    }
}