using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncRotating(GameHooksService owner) : IGameHookDatamapCFuncRotating
{
    internal readonly CFuncRotatingHurtTouchHook CFuncRotatingHurtTouchHook = new(owner);
    internal readonly CFuncRotatingReverseMoveHook CFuncRotatingReverseMoveHook = new(owner);
    internal readonly CFuncRotatingRotateMoveHook CFuncRotatingRotateMoveHook = new(owner);
    internal readonly CFuncRotatingRotatingUseHook CFuncRotatingRotatingUseHook = new(owner);
    internal readonly CFuncRotatingSpinDownMoveHook CFuncRotatingSpinDownMoveHook = new(owner);
    internal readonly CFuncRotatingSpinUpMoveHook CFuncRotatingSpinUpMoveHook = new(owner);

    public ICFuncRotatingHurtTouchHook HurtTouch => CFuncRotatingHurtTouchHook;
    public ICFuncRotatingReverseMoveHook ReverseMove => CFuncRotatingReverseMoveHook;
    public ICFuncRotatingRotateMoveHook RotateMove => CFuncRotatingRotateMoveHook;
    public ICFuncRotatingRotatingUseHook RotatingUse => CFuncRotatingRotatingUseHook;
    public ICFuncRotatingSpinDownMoveHook SpinDownMove => CFuncRotatingSpinDownMoveHook;
    public ICFuncRotatingSpinUpMoveHook SpinUpMove => CFuncRotatingSpinUpMoveHook;

    internal void UnregisterListeners()
    {
        CFuncRotatingHurtTouchHook.UnregisterListeners();
        CFuncRotatingReverseMoveHook.UnregisterListeners();
        CFuncRotatingRotateMoveHook.UnregisterListeners();
        CFuncRotatingRotatingUseHook.UnregisterListeners();
        CFuncRotatingSpinDownMoveHook.UnregisterListeners();
        CFuncRotatingSpinUpMoveHook.UnregisterListeners();
    }
}