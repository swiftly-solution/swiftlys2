using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundEventPathCornerEntity : IGameHookDatamapCSoundEventPathCornerEntity
{
    internal readonly CSoundEventPathCornerEntitySoundEventPathCornerThinkHook CSoundEventPathCornerEntitySoundEventPathCornerThinkHook = new();

    public ICSoundEventPathCornerEntitySoundEventPathCornerThinkHook SoundEventPathCornerThink => CSoundEventPathCornerEntitySoundEventPathCornerThinkHook;

    internal void UnregisterListeners()
    {
        CSoundEventPathCornerEntitySoundEventPathCornerThinkHook.UnregisterListeners();
    }
}