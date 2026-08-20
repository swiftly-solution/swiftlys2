using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundEventEntity : IGameHookDatamapCSoundEventEntity
{
    internal readonly CSoundEventEntitySoundFinishedThinkHook CSoundEventEntitySoundFinishedThinkHook = new();

    public ICSoundEventEntitySoundFinishedThinkHook SoundFinishedThink => CSoundEventEntitySoundFinishedThinkHook;

    internal void UnregisterListeners()
    {
        CSoundEventEntitySoundFinishedThinkHook.UnregisterListeners();
    }
}