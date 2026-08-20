using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPhysSlideConstraint : IGameHookDatamapCPhysSlideConstraint
{
    internal readonly CPhysSlideConstraintSoundThinkHook CPhysSlideConstraintSoundThinkHook = new();

    public ICPhysSlideConstraintSoundThinkHook SoundThink => CPhysSlideConstraintSoundThinkHook;

    internal void UnregisterListeners()
    {
        CPhysSlideConstraintSoundThinkHook.UnregisterListeners();
    }
}