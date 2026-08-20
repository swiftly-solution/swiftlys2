using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CEntityDissolveDissolveThinkPreContext
{
    public CEntityDissolve SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CEntityDissolveDissolveThinkPostContext
{
    public CEntityDissolve SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCEntityDissolveDissolveThinkPreDelegate(ref CEntityDissolveDissolveThinkPreContext ctx);
public delegate void OnCEntityDissolveDissolveThinkPostDelegate(ref CEntityDissolveDissolveThinkPostContext ctx);

public interface ICEntityDissolveDissolveThinkHook
{
    public event OnCEntityDissolveDissolveThinkPreDelegate Pre;
    public event OnCEntityDissolveDissolveThinkPostDelegate Post;

    public void Invoke(CEntityDissolve schemaObject);
}