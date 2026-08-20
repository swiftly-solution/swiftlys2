using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCCSPlayerResource : IGameHookDatamapCCSPlayerResource
{
    internal readonly CCSPlayerResourceResourceThinkHook CCSPlayerResourceResourceThinkHook = new();

    public ICCSPlayerResourceResourceThinkHook ResourceThink => CCSPlayerResourceResourceThinkHook;

    internal void UnregisterListeners()
    {
        CCSPlayerResourceResourceThinkHook.UnregisterListeners();
    }
}