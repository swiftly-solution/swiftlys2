using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysicalButtonButtonBackHomePreContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysicalButtonButtonBackHomePostContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysicalButtonButtonBackHomePreDelegate(ref CPhysicalButtonButtonBackHomePreContext ctx);
public delegate void OnCPhysicalButtonButtonBackHomePostDelegate(ref CPhysicalButtonButtonBackHomePostContext ctx);

public interface ICPhysicalButtonButtonBackHomeHook
{
    public event OnCPhysicalButtonButtonBackHomePreDelegate Pre;
    public event OnCPhysicalButtonButtonBackHomePostDelegate Post;

    public void Invoke(CPhysicalButton schemaObject);
}