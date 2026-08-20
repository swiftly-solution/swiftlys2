using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPointPush : IGameHookDatamapCPointPush
{
    internal readonly CPointPushPushThinkHook CPointPushPushThinkHook = new();

    public ICPointPushPushThinkHook PushThink => CPointPushPushThinkHook;

    internal void UnregisterListeners()
    {
        CPointPushPushThinkHook.UnregisterListeners();
    }
}