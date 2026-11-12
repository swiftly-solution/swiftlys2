using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundEventEntity(GameHooksService owner) : IGameHookDatamapCSoundEventEntity
{
    internal readonly CSoundEventEntitySoundFinishedThinkHook CSoundEventEntitySoundFinishedThinkHook = new(owner);

    public ICSoundEventEntitySoundFinishedThinkHook SoundFinishedThink => CSoundEventEntitySoundFinishedThinkHook;

    internal void UnregisterListeners()
    {
        CSoundEventEntitySoundFinishedThinkHook.UnregisterListeners();
    }
}