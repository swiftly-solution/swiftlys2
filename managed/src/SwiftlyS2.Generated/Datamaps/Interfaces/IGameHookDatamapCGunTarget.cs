namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCGunTarget
{
    public ICGunTargetNextHook Next { get; }
    public ICGunTargetStartHook Start { get; }
    public ICGunTargetWaitHook Wait { get; }
}