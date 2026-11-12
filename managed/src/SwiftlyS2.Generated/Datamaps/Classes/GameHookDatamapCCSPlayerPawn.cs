using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCCSPlayerPawn(GameHooksService owner) : IGameHookDatamapCCSPlayerPawn
{
    internal readonly CCSPlayerPawnCheckStuffThinkHook CCSPlayerPawnCheckStuffThinkHook = new(owner);
    internal readonly CCSPlayerPawnPushawayThinkHook CCSPlayerPawnPushawayThinkHook = new(owner);

    public ICCSPlayerPawnCheckStuffThinkHook CheckStuffThink => CCSPlayerPawnCheckStuffThinkHook;
    public ICCSPlayerPawnPushawayThinkHook PushawayThink => CCSPlayerPawnPushawayThinkHook;

    internal void UnregisterListeners()
    {
        CCSPlayerPawnCheckStuffThinkHook.UnregisterListeners();
        CCSPlayerPawnPushawayThinkHook.UnregisterListeners();
    }
}