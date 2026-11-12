using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCCSPlayerController(GameHooksService owner) : IGameHookDatamapCCSPlayerController
{
    internal readonly CCSPlayerControllerInventoryUpdateThinkHook CCSPlayerControllerInventoryUpdateThinkHook = new(owner);
    internal readonly CCSPlayerControllerPlayerForceTeamThinkHook CCSPlayerControllerPlayerForceTeamThinkHook = new(owner);
    internal readonly CCSPlayerControllerResetForceTeamThinkHook CCSPlayerControllerResetForceTeamThinkHook = new(owner);
    internal readonly CCSPlayerControllerResourceDataThinkHook CCSPlayerControllerResourceDataThinkHook = new(owner);

    public ICCSPlayerControllerInventoryUpdateThinkHook InventoryUpdateThink => CCSPlayerControllerInventoryUpdateThinkHook;
    public ICCSPlayerControllerPlayerForceTeamThinkHook PlayerForceTeamThink => CCSPlayerControllerPlayerForceTeamThinkHook;
    public ICCSPlayerControllerResetForceTeamThinkHook ResetForceTeamThink => CCSPlayerControllerResetForceTeamThinkHook;
    public ICCSPlayerControllerResourceDataThinkHook ResourceDataThink => CCSPlayerControllerResourceDataThinkHook;

    internal void UnregisterListeners()
    {
        CCSPlayerControllerInventoryUpdateThinkHook.UnregisterListeners();
        CCSPlayerControllerPlayerForceTeamThinkHook.UnregisterListeners();
        CCSPlayerControllerResetForceTeamThinkHook.UnregisterListeners();
        CCSPlayerControllerResourceDataThinkHook.UnregisterListeners();
    }
}