using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPhysicalButton(GameHooksService owner) : IGameHookDatamapCPhysicalButton
{
    internal readonly CPhysicalButtonButtonActivateHook CPhysicalButtonButtonActivateHook = new(owner);
    internal readonly CPhysicalButtonButtonBackHomeHook CPhysicalButtonButtonBackHomeHook = new(owner);
    internal readonly CPhysicalButtonButtonReturnHook CPhysicalButtonButtonReturnHook = new(owner);
    internal readonly CPhysicalButtonButtonTouchHook CPhysicalButtonButtonTouchHook = new(owner);
    internal readonly CPhysicalButtonEndTouchHook CPhysicalButtonEndTouchHook = new(owner);
    internal readonly CPhysicalButtonPhysicsThinkHook CPhysicalButtonPhysicsThinkHook = new(owner);
    internal readonly CPhysicalButtonSetupButtonHook CPhysicalButtonSetupButtonHook = new(owner);
    internal readonly CPhysicalButtonTriggerAndWaitHook CPhysicalButtonTriggerAndWaitHook = new(owner);

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