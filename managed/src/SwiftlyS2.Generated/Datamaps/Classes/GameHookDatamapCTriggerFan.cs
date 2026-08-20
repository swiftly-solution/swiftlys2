using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerFan : IGameHookDatamapCTriggerFan
{
    internal readonly CTriggerFanPushThinkHook CTriggerFanPushThinkHook = new();

    public ICTriggerFanPushThinkHook PushThink => CTriggerFanPushThinkHook;

    internal void UnregisterListeners()
    {
        CTriggerFanPushThinkHook.UnregisterListeners();
    }
}