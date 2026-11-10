using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPointPush(GameHooksService owner) : IGameHookDatamapCPointPush
{
    internal readonly CPointPushPushThinkHook CPointPushPushThinkHook = new(owner);

    public ICPointPushPushThinkHook PushThink => CPointPushPushThinkHook;

    internal void UnregisterListeners()
    {
        CPointPushPushThinkHook.UnregisterListeners();
    }
}