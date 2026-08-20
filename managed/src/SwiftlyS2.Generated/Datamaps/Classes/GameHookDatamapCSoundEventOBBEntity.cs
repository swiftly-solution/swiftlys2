using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundEventOBBEntity : IGameHookDatamapCSoundEventOBBEntity
{
    internal readonly CSoundEventOBBEntitySoundEventOBBThinkHook CSoundEventOBBEntitySoundEventOBBThinkHook = new();

    public ICSoundEventOBBEntitySoundEventOBBThinkHook SoundEventOBBThink => CSoundEventOBBEntitySoundEventOBBThinkHook;

    internal void UnregisterListeners()
    {
        CSoundEventOBBEntitySoundEventOBBThinkHook.UnregisterListeners();
    }
}