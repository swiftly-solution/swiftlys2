using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CPhysicalButtonSetupButtonPreContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CPhysicalButtonSetupButtonPostContext
{
    public CPhysicalButton SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCPhysicalButtonSetupButtonPreDelegate(ref CPhysicalButtonSetupButtonPreContext ctx);
public delegate void OnCPhysicalButtonSetupButtonPostDelegate(ref CPhysicalButtonSetupButtonPostContext ctx);

public interface ICPhysicalButtonSetupButtonHook
{
    public event OnCPhysicalButtonSetupButtonPreDelegate Pre;
    public event OnCPhysicalButtonSetupButtonPostDelegate Post;

    public void Invoke(CPhysicalButton schemaObject);
}