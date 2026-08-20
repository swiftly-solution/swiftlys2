namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCFuncRotating
{
    public ICFuncRotatingHurtTouchHook HurtTouch { get; }
    public ICFuncRotatingReverseMoveHook ReverseMove { get; }
    public ICFuncRotatingRotateMoveHook RotateMove { get; }
    public ICFuncRotatingRotatingUseHook RotatingUse { get; }
    public ICFuncRotatingSpinDownMoveHook SpinDownMove { get; }
    public ICFuncRotatingSpinUpMoveHook SpinUpMove { get; }
}