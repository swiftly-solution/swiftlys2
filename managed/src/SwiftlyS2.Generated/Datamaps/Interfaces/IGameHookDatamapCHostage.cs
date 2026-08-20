namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCHostage
{
    public ICHostageHostageThinkHook HostageThink { get; }
    public ICHostageHostageUseHook HostageUse { get; }
}