using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPointOrient(GameHooksService owner) : IGameHookDatamapCPointOrient
{
    internal readonly CPointOrientReorientThinkHook CPointOrientReorientThinkHook = new(owner);

    public ICPointOrientReorientThinkHook ReorientThink => CPointOrientReorientThinkHook;

    internal void UnregisterListeners()
    {
        CPointOrientReorientThinkHook.UnregisterListeners();
    }
}