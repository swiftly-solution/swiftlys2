using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncRotating : IGameHookDatamapCFuncRotating
{
    internal readonly CFuncRotatingHurtTouchHook CFuncRotatingHurtTouchHook = new();
    internal readonly CFuncRotatingReverseMoveHook CFuncRotatingReverseMoveHook = new();
    internal readonly CFuncRotatingRotateMoveHook CFuncRotatingRotateMoveHook = new();
    internal readonly CFuncRotatingRotatingUseHook CFuncRotatingRotatingUseHook = new();
    internal readonly CFuncRotatingSpinDownMoveHook CFuncRotatingSpinDownMoveHook = new();
    internal readonly CFuncRotatingSpinUpMoveHook CFuncRotatingSpinUpMoveHook = new();

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