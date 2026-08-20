using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CFuncRotatingHurtTouchPreContext
{
    public CFuncRotating SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CFuncRotatingHurtTouchPostContext
{
    public CFuncRotating SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCFuncRotatingHurtTouchPreDelegate(ref CFuncRotatingHurtTouchPreContext ctx);
public delegate void OnCFuncRotatingHurtTouchPostDelegate(ref CFuncRotatingHurtTouchPostContext ctx);

public interface ICFuncRotatingHurtTouchHook
{
    public event OnCFuncRotatingHurtTouchPreDelegate Pre;
    public event OnCFuncRotatingHurtTouchPostDelegate Post;

    public void Invoke(CFuncRotating schemaObject);
}