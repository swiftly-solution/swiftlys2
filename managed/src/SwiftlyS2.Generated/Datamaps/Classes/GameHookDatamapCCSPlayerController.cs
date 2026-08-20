using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCCSPlayerController : IGameHookDatamapCCSPlayerController
{
    internal readonly CCSPlayerControllerInventoryUpdateThinkHook CCSPlayerControllerInventoryUpdateThinkHook = new();
    internal readonly CCSPlayerControllerPlayerForceTeamThinkHook CCSPlayerControllerPlayerForceTeamThinkHook = new();
    internal readonly CCSPlayerControllerResetForceTeamThinkHook CCSPlayerControllerResetForceTeamThinkHook = new();
    internal readonly CCSPlayerControllerResourceDataThinkHook CCSPlayerControllerResourceDataThinkHook = new();

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