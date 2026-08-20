using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEntityDissolve : IGameHookDatamapCEntityDissolve
{
    internal readonly CEntityDissolveDissolveThinkHook CEntityDissolveDissolveThinkHook = new();
    internal readonly CEntityDissolveElectrocuteThinkHook CEntityDissolveElectrocuteThinkHook = new();

    public ICEntityDissolveDissolveThinkHook DissolveThink => CEntityDissolveDissolveThinkHook;
    public ICEntityDissolveElectrocuteThinkHook ElectrocuteThink => CEntityDissolveElectrocuteThinkHook;

    internal void UnregisterListeners()
    {
        CEntityDissolveDissolveThinkHook.UnregisterListeners();
        CEntityDissolveElectrocuteThinkHook.UnregisterListeners();
    }
}