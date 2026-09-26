using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysicalButtonButtonActivatePreContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysicalButtonButtonActivatePostContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysicalButtonButtonActivatePreDelegate(ref CPhysicalButtonButtonActivatePreContext ctx);
public delegate void OnCPhysicalButtonButtonActivatePostDelegate(ref CPhysicalButtonButtonActivatePostContext ctx);

public interface ICPhysicalButtonButtonActivateHook
{
    public event OnCPhysicalButtonButtonActivatePreDelegate Pre;
    public event OnCPhysicalButtonButtonActivatePostDelegate Post;

    public void Invoke(CPhysicalButton schemaObject);
}