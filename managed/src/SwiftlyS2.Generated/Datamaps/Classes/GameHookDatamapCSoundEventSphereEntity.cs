using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundEventSphereEntity : IGameHookDatamapCSoundEventSphereEntity
{
    internal readonly CSoundEventSphereEntitySoundEventSphereThinkHook CSoundEventSphereEntitySoundEventSphereThinkHook = new();

    public ICSoundEventSphereEntitySoundEventSphereThinkHook SoundEventSphereThink => CSoundEventSphereEntitySoundEventSphereThinkHook;

    internal void UnregisterListeners()
    {
        CSoundEventSphereEntitySoundEventSphereThinkHook.UnregisterListeners();
    }
}