using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundEventConeEntity(GameHooksService owner) : IGameHookDatamapCSoundEventConeEntity
{
    internal readonly CSoundEventConeEntitySoundEventConeThinkHook CSoundEventConeEntitySoundEventConeThinkHook = new(owner);

    public ICSoundEventConeEntitySoundEventConeThinkHook SoundEventConeThink => CSoundEventConeEntitySoundEventConeThinkHook;

    internal void UnregisterListeners()
    {
        CSoundEventConeEntitySoundEventConeThinkHook.UnregisterListeners();
    }
}