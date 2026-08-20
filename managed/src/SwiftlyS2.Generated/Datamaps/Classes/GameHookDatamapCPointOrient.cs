using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPointOrient : IGameHookDatamapCPointOrient
{
    internal readonly CPointOrientReorientThinkHook CPointOrientReorientThinkHook = new();

    public ICPointOrientReorientThinkHook ReorientThink => CPointOrientReorientThinkHook;

    internal void UnregisterListeners()
    {
        CPointOrientReorientThinkHook.UnregisterListeners();
    }
}