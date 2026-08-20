using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCCSPlayerPawn : IGameHookDatamapCCSPlayerPawn
{
    internal readonly CCSPlayerPawnCheckStuffThinkHook CCSPlayerPawnCheckStuffThinkHook = new();
    internal readonly CCSPlayerPawnPushawayThinkHook CCSPlayerPawnPushawayThinkHook = new();

    public ICCSPlayerPawnCheckStuffThinkHook CheckStuffThink => CCSPlayerPawnCheckStuffThinkHook;
    public ICCSPlayerPawnPushawayThinkHook PushawayThink => CCSPlayerPawnPushawayThinkHook;

    internal void UnregisterListeners()
    {
        CCSPlayerPawnCheckStuffThinkHook.UnregisterListeners();
        CCSPlayerPawnPushawayThinkHook.UnregisterListeners();
    }
}