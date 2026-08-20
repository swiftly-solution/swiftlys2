using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CGunTargetStartPreContext
{
    public CGunTarget SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CGunTargetStartPostContext
{
    public CGunTarget SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCGunTargetStartPreDelegate(ref CGunTargetStartPreContext ctx);
public delegate void OnCGunTargetStartPostDelegate(ref CGunTargetStartPostContext ctx);

public interface ICGunTargetStartHook
{
    public event OnCGunTargetStartPreDelegate Pre;
    public event OnCGunTargetStartPostDelegate Post;

    public void Invoke(CGunTarget schemaObject);
}