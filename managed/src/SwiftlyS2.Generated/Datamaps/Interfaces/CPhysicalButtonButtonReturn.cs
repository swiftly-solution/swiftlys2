using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysicalButtonButtonReturnPreContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysicalButtonButtonReturnPostContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysicalButtonButtonReturnPreDelegate(ref CPhysicalButtonButtonReturnPreContext ctx);
public delegate void OnCPhysicalButtonButtonReturnPostDelegate(ref CPhysicalButtonButtonReturnPostContext ctx);

public interface ICPhysicalButtonButtonReturnHook
{
    public event OnCPhysicalButtonButtonReturnPreDelegate Pre;
    public event OnCPhysicalButtonButtonReturnPostDelegate Post;

    public void Invoke(CPhysicalButton schemaObject);
}