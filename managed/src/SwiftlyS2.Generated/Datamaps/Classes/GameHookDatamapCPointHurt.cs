using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPointHurt(GameHooksService owner) : IGameHookDatamapCPointHurt
{
    internal readonly CPointHurtHurtThinkHook CPointHurtHurtThinkHook = new(owner);

    public ICPointHurtHurtThinkHook HurtThink => CPointHurtHurtThinkHook;

    internal void UnregisterListeners()
    {
        CPointHurtHurtThinkHook.UnregisterListeners();
    }
}