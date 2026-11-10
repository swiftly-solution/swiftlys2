using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCCSPlayerResource(GameHooksService owner) : IGameHookDatamapCCSPlayerResource
{
    internal readonly CCSPlayerResourceResourceThinkHook CCSPlayerResourceResourceThinkHook = new(owner);

    public ICCSPlayerResourceResourceThinkHook ResourceThink => CCSPlayerResourceResourceThinkHook;

    internal void UnregisterListeners()
    {
        CCSPlayerResourceResourceThinkHook.UnregisterListeners();
    }
}