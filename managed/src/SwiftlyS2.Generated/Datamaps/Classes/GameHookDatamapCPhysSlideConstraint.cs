using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPhysSlideConstraint(GameHooksService owner) : IGameHookDatamapCPhysSlideConstraint
{
    internal readonly CPhysSlideConstraintSoundThinkHook CPhysSlideConstraintSoundThinkHook = new(owner);

    public ICPhysSlideConstraintSoundThinkHook SoundThink => CPhysSlideConstraintSoundThinkHook;

    internal void UnregisterListeners()
    {
        CPhysSlideConstraintSoundThinkHook.UnregisterListeners();
    }
}