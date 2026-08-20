namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCEntityDissolve
{
    public ICEntityDissolveDissolveThinkHook DissolveThink { get; }
    public ICEntityDissolveElectrocuteThinkHook ElectrocuteThink { get; }
}