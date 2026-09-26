namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCPhysicalButton
{
    public ICPhysicalButtonButtonActivateHook ButtonActivate { get; }
    public ICPhysicalButtonButtonBackHomeHook ButtonBackHome { get; }
    public ICPhysicalButtonButtonReturnHook ButtonReturn { get; }
    public ICPhysicalButtonButtonTouchHook ButtonTouch { get; }
    public ICPhysicalButtonEndTouchHook EndTouch { get; }
    public ICPhysicalButtonPhysicsThinkHook PhysicsThink { get; }
    public ICPhysicalButtonSetupButtonHook SetupButton { get; }
    public ICPhysicalButtonTriggerAndWaitHook TriggerAndWait { get; }
}