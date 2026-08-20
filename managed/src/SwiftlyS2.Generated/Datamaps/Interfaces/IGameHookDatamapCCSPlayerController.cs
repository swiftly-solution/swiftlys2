namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCCSPlayerController
{
    public ICCSPlayerControllerInventoryUpdateThinkHook InventoryUpdateThink { get; }
    public ICCSPlayerControllerPlayerForceTeamThinkHook PlayerForceTeamThink { get; }
    public ICCSPlayerControllerResetForceTeamThinkHook ResetForceTeamThink { get; }
    public ICCSPlayerControllerResourceDataThinkHook ResourceDataThink { get; }
}