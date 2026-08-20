namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCItemDefuser
{
    public ICItemDefuserActivateThinkHook ActivateThink { get; }
    public ICItemDefuserDefuserTouchHook DefuserTouch { get; }
}