using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CGunTargetWaitPreContext
{
    public CGunTarget SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CGunTargetWaitPostContext
{
    public CGunTarget SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCGunTargetWaitPreDelegate(ref CGunTargetWaitPreContext ctx);
public delegate void OnCGunTargetWaitPostDelegate(ref CGunTargetWaitPostContext ctx);

public interface ICGunTargetWaitHook
{
    public event OnCGunTargetWaitPreDelegate Pre;
    public event OnCGunTargetWaitPostDelegate Post;

    public void Invoke(CGunTarget schemaObject);
}