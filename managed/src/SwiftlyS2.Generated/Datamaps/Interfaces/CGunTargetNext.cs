using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CGunTargetNextPreContext
{
    public CGunTarget SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CGunTargetNextPostContext
{
    public CGunTarget SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCGunTargetNextPreDelegate(ref CGunTargetNextPreContext ctx);
public delegate void OnCGunTargetNextPostDelegate(ref CGunTargetNextPostContext ctx);

public interface ICGunTargetNextHook
{
    public event OnCGunTargetNextPreDelegate Pre;
    public event OnCGunTargetNextPostDelegate Post;

    public void Invoke(CGunTarget schemaObject);
}