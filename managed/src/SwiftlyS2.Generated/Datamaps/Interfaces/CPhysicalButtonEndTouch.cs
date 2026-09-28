using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysicalButtonEndTouchPreContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysicalButtonEndTouchPostContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysicalButtonEndTouchPreDelegate(ref CPhysicalButtonEndTouchPreContext ctx);
public delegate void OnCPhysicalButtonEndTouchPostDelegate(ref CPhysicalButtonEndTouchPostContext ctx);

public interface ICPhysicalButtonEndTouchHook
{
    public event OnCPhysicalButtonEndTouchPreDelegate Pre;
    public event OnCPhysicalButtonEndTouchPostDelegate Post;

    public void Invoke(CPhysicalButton schemaObject);
}