using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CGenericConstraintUpdateThinkPreContext
{
    public CGenericConstraint SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CGenericConstraintUpdateThinkPostContext
{
    public CGenericConstraint SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCGenericConstraintUpdateThinkPreDelegate(ref CGenericConstraintUpdateThinkPreContext ctx);
public delegate void OnCGenericConstraintUpdateThinkPostDelegate(ref CGenericConstraintUpdateThinkPostContext ctx);

public interface ICGenericConstraintUpdateThinkHook
{
    public event OnCGenericConstraintUpdateThinkPreDelegate Pre;
    public event OnCGenericConstraintUpdateThinkPostDelegate Post;

    public void Invoke(CGenericConstraint schemaObject);
}