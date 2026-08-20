using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CSplineConstraintTransitionThinkPreContext
{
    public CSplineConstraint SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CSplineConstraintTransitionThinkPostContext
{
    public CSplineConstraint SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCSplineConstraintTransitionThinkPreDelegate(ref CSplineConstraintTransitionThinkPreContext ctx);
public delegate void OnCSplineConstraintTransitionThinkPostDelegate(ref CSplineConstraintTransitionThinkPostContext ctx);

public interface ICSplineConstraintTransitionThinkHook
{
    public event OnCSplineConstraintTransitionThinkPreDelegate Pre;
    public event OnCSplineConstraintTransitionThinkPostDelegate Post;

    public void Invoke(CSplineConstraint schemaObject);
}