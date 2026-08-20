using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundEventConeEntity : IGameHookDatamapCSoundEventConeEntity
{
    internal readonly CSoundEventConeEntitySoundEventConeThinkHook CSoundEventConeEntitySoundEventConeThinkHook = new();

    public ICSoundEventConeEntitySoundEventConeThinkHook SoundEventConeThink => CSoundEventConeEntitySoundEventConeThinkHook;

    internal void UnregisterListeners()
    {
        CSoundEventConeEntitySoundEventConeThinkHook.UnregisterListeners();
    }
}