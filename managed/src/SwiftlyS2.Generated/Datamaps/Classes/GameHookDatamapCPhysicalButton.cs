using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPhysicalButton : IGameHookDatamapCPhysicalButton
{
    internal readonly CPhysicalButtonButtonActivateHook CPhysicalButtonButtonActivateHook = new();
    internal readonly CPhysicalButtonButtonBackHomeHook CPhysicalButtonButtonBackHomeHook = new();
    internal readonly CPhysicalButtonButtonReturnHook CPhysicalButtonButtonReturnHook = new();
    internal readonly CPhysicalButtonButtonTouchHook CPhysicalButtonButtonTouchHook = new();
    internal readonly CPhysicalButtonEndTouchHook CPhysicalButtonEndTouchHook = new();
    internal readonly CPhysicalButtonPhysicsThinkHook CPhysicalButtonPhysicsThinkHook = new();
    internal readonly CPhysicalButtonSetupButtonHook CPhysicalButtonSetupButtonHook = new();
    internal readonly CPhysicalButtonTriggerAndWaitHook CPhysicalButtonTriggerAndWaitHook = new();

    public ICPhysicalButtonButtonActivateHook ButtonActivate => CPhysicalButtonButtonActivateHook;
    public ICPhysicalButtonButtonBackHomeHook ButtonBackHome => CPhysicalButtonButtonBackHomeHook;
    public ICPhysicalButtonButtonReturnHook ButtonReturn => CPhysicalButtonButtonReturnHook;
    public ICPhysicalButtonButtonTouchHook ButtonTouch => CPhysicalButtonButtonTouchHook;
    public ICPhysicalButtonEndTouchHook EndTouch => CPhysicalButtonEndTouchHook;
    public ICPhysicalButtonPhysicsThinkHook PhysicsThink => CPhysicalButtonPhysicsThinkHook;
    public ICPhysicalButtonSetupButtonHook SetupButton => CPhysicalButtonSetupButtonHook;
    public ICPhysicalButtonTriggerAndWaitHook TriggerAndWait => CPhysicalButtonTriggerAndWaitHook;

    internal void UnregisterListeners()
    {
        CPhysicalButtonButtonActivateHook.UnregisterListeners();
        CPhysicalButtonButtonBackHomeHook.UnregisterListeners();
        CPhysicalButtonButtonReturnHook.UnregisterListeners();
        CPhysicalButtonButtonTouchHook.UnregisterListeners();
        CPhysicalButtonEndTouchHook.UnregisterListeners();
        CPhysicalButtonPhysicsThinkHook.UnregisterListeners();
        CPhysicalButtonSetupButtonHook.UnregisterListeners();
        CPhysicalButtonTriggerAndWaitHook.UnregisterListeners();
    }
}