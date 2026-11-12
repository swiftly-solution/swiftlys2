using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEntityDissolve(GameHooksService owner) : IGameHookDatamapCEntityDissolve
{
    internal readonly CEntityDissolveDissolveThinkHook CEntityDissolveDissolveThinkHook = new(owner);
    internal readonly CEntityDissolveElectrocuteThinkHook CEntityDissolveElectrocuteThinkHook = new(owner);

    public ICEntityDissolveDissolveThinkHook DissolveThink => CEntityDissolveDissolveThinkHook;
    public ICEntityDissolveElectrocuteThinkHook ElectrocuteThink => CEntityDissolveElectrocuteThinkHook;

    internal void UnregisterListeners()
    {
        CEntityDissolveDissolveThinkHook.UnregisterListeners();
        CEntityDissolveElectrocuteThinkHook.UnregisterListeners();
    }
}