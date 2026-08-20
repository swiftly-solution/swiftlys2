using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public ref struct CEntityDissolveElectrocuteThinkPreContext
{
    public CEntityDissolve SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public ref struct CEntityDissolveElectrocuteThinkPostContext
{
    public CEntityDissolve SchemaObject;
    private HookResult _hookResult;
    public void SetHookResult(HookResult result) => _hookResult = result;
    internal HookResult HookResult => _hookResult;
}

public delegate void OnCEntityDissolveElectrocuteThinkPreDelegate(ref CEntityDissolveElectrocuteThinkPreContext ctx);
public delegate void OnCEntityDissolveElectrocuteThinkPostDelegate(ref CEntityDissolveElectrocuteThinkPostContext ctx);

public interface ICEntityDissolveElectrocuteThinkHook
{
    public event OnCEntityDissolveElectrocuteThinkPreDelegate Pre;
    public event OnCEntityDissolveElectrocuteThinkPostDelegate Post;

    public void Invoke(CEntityDissolve schemaObject);
}