using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPointHurt : IGameHookDatamapCPointHurt
{
    internal readonly CPointHurtHurtThinkHook CPointHurtHurtThinkHook = new();

    public ICPointHurtHurtThinkHook HurtThink => CPointHurtHurtThinkHook;

    internal void UnregisterListeners()
    {
        CPointHurtHurtThinkHook.UnregisterListeners();
    }
}