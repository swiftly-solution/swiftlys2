namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCFuncTrain
{
    public ICFuncTrainNextHook Next { get; }
    public ICFuncTrainWaitHook Wait { get; }
}