using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundEventOBBEntity(GameHooksService owner) : IGameHookDatamapCSoundEventOBBEntity
{
    internal readonly CSoundEventOBBEntitySoundEventOBBThinkHook CSoundEventOBBEntitySoundEventOBBThinkHook = new(owner);

    public ICSoundEventOBBEntitySoundEventOBBThinkHook SoundEventOBBThink => CSoundEventOBBEntitySoundEventOBBThinkHook;

    internal void UnregisterListeners()
    {
        CSoundEventOBBEntitySoundEventOBBThinkHook.UnregisterListeners();
    }
}