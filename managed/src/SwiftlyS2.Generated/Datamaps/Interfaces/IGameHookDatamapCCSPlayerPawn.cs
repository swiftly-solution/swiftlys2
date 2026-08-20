namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCCSPlayerPawn
{
    public ICCSPlayerPawnCheckStuffThinkHook CheckStuffThink { get; }
    public ICCSPlayerPawnPushawayThinkHook PushawayThink { get; }
}