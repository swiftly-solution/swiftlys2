using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundEventSphereEntity(GameHooksService owner) : IGameHookDatamapCSoundEventSphereEntity
{
    internal readonly CSoundEventSphereEntitySoundEventSphereThinkHook CSoundEventSphereEntitySoundEventSphereThinkHook = new(owner);

    public ICSoundEventSphereEntitySoundEventSphereThinkHook SoundEventSphereThink => CSoundEventSphereEntitySoundEventSphereThinkHook;

    internal void UnregisterListeners()
    {
        CSoundEventSphereEntitySoundEventSphereThinkHook.UnregisterListeners();
    }
}