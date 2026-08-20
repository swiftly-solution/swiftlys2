using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundOpvarSetOBBWindEntity : IGameHookDatamapCSoundOpvarSetOBBWindEntity
{
    internal readonly CSoundOpvarSetOBBWindEntitySetOpvarThinkHook CSoundOpvarSetOBBWindEntitySetOpvarThinkHook = new();

    public ICSoundOpvarSetOBBWindEntitySetOpvarThinkHook SetOpvarThink => CSoundOpvarSetOBBWindEntitySetOpvarThinkHook;

    internal void UnregisterListeners()
    {
        CSoundOpvarSetOBBWindEntitySetOpvarThinkHook.UnregisterListeners();
    }
}