using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerFan(GameHooksService owner) : IGameHookDatamapCTriggerFan
{
    internal readonly CTriggerFanPushThinkHook CTriggerFanPushThinkHook = new(owner);

    public ICTriggerFanPushThinkHook PushThink => CTriggerFanPushThinkHook;

    internal void UnregisterListeners()
    {
        CTriggerFanPushThinkHook.UnregisterListeners();
    }
}