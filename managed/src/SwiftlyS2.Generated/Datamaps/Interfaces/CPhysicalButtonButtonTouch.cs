using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysicalButtonButtonTouchPreContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysicalButtonButtonTouchPostContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysicalButtonButtonTouchPreDelegate(ref CPhysicalButtonButtonTouchPreContext ctx);
public delegate void OnCPhysicalButtonButtonTouchPostDelegate(ref CPhysicalButtonButtonTouchPostContext ctx);

public interface ICPhysicalButtonButtonTouchHook
{
    public event OnCPhysicalButtonButtonTouchPreDelegate Pre;
    public event OnCPhysicalButtonButtonTouchPostDelegate Post;

    public void Invoke(CPhysicalButton schemaObject);
}